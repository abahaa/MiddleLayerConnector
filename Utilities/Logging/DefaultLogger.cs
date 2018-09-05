
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CodeLab.Barq.BackEndConnector.Utilities.Logging
{
    public class DefaultLogger
    {
        #region private-variables
        static string ConfigFileName { get; set; } = null;

        static object  configFileLocker = new object();

        private static Logger CurrentLogger
        {
            get
            {
                if (_currentLogger == null)
                {
                    Init();
                }
                return _currentLogger;

            }
        }

        private static object initializationLocker = new object();

        private static Logger _currentLogger;
        

        private const string CONFIG_FILE_KEY_NAME = "NlogConfigFile";

        private const string LOCAL_MODULE_NAME = "INSIDE_LOGGER";

        private const string ROOTPATH_VAR_NAME = "rootlogdir";

        #endregion

        #region properties

        private static string AbsoluteConfigurationFilePath
        {
            get
            {
                string absoluteFilePath = ConfigFileName;
                if (!string.IsNullOrEmpty(ConfigFileName))
                {
                    if (!Path.IsPathRooted(ConfigFileName)) //not an absolute path
                    {
                        absoluteFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ConfigFileName);
                    }
                }
                return absoluteFilePath;
            }
        }

        #endregion

        #region consturctors

        public static void Initialize(string configFilePath)
        {
            lock (configFileLocker)
            {
                if (!string.IsNullOrEmpty(ConfigFileName))
                {
                    throw new LoggerExcetion(string.Format("{0}:Config file path has already been set and cannot be changed.", LOCAL_MODULE_NAME, AbsoluteConfigurationFilePath),
                             LoggerFailureType.ConfigFailure);
                }
                ConfigFileName = configFilePath;
            }
            Init();
        }

        static void Init()
        {
            lock (initializationLocker)
            {
                if (_currentLogger == null)
                {
                    if (string.IsNullOrEmpty(AbsoluteConfigurationFilePath))
                    {
                        throw new LoggerExcetion(string.Format("{0}:Configuration file name is not set in application configuration", LOCAL_MODULE_NAME),
                            LoggerFailureType.ConfigFailure);
                    }
                    if (!File.Exists(AbsoluteConfigurationFilePath))
                    {
                        throw new LoggerExcetion(string.Format("{0}:Configuration file named {1} does not exist", LOCAL_MODULE_NAME, AbsoluteConfigurationFilePath),
                            LoggerFailureType.ConfigFailure);
                    }
                    try
                    {
                        LogManager.ThrowConfigExceptions = true;
                        LogManager.Configuration = new NLog.Config.XmlLoggingConfiguration(AbsoluteConfigurationFilePath, false);

                        _currentLogger = LogManager.GetCurrentClassLogger();

                        string rootpath = LogManager.Configuration.Variables[ROOTPATH_VAR_NAME] == null ? null : LogManager.Configuration.Variables[ROOTPATH_VAR_NAME].Text;

                        if (string.IsNullOrEmpty(rootpath))
                        {
                            throw new LoggerExcetion(string.Format("{0}:Root Directory path variable is null or empty", LOCAL_MODULE_NAME),
                            LoggerFailureType.ConfigFailure);
                        }

                        if (!Directory.Exists(rootpath))
                        {
                            try
                            {
                                Directory.CreateDirectory(rootpath);
                            }
                            catch (Exception ex)
                            {
                                throw new LoggerExcetion(string.Format("{0}:Failed to create root logging folder with path {1}", LOCAL_MODULE_NAME, rootpath), ex,
                            LoggerFailureType.ConfigFailure);
                            }
                        }


                    }
                    catch (Exception ex)
                    {
                        throw new LoggerExcetion(string.Format("{0}:failed to initialize log configuration from file {1}", LOCAL_MODULE_NAME, AbsoluteConfigurationFilePath), ex,
                            LoggerFailureType.ConfigFailure);
                    }
                    try
                    {
                        CurrentLogger.Info("Logging Started");
                    }
                    catch (Exception ex)
                    {
                        throw new LoggerExcetion(string.Format("{0}:failed to write initializarion log message", LOCAL_MODULE_NAME), ex,
                            LoggerFailureType.WriteFailure);
                    }
                }
            }
        }

        #endregion

        #region private-methods

        /// <summary>
        /// builds the message string according to message format and parameters and the logParamwers objects
        /// </summary>
        /// <param name="logObjectsArr">array of objects to log their values for custom message user must override toSting() method</param>
        /// <param name="messageFormat">format of the message to be logged</param>
        /// <param name="messageParameters"> the parameters to fill the message placeholders</param>
        /// <returns>returns the complete message to be logged</returns>
        private static string CreateLogMessage(object[] logObjectsArr, string messageFormat, string callerInfo, object[] messageParameters)
        {
            string finalMessage = string.Empty;
            //Test Shelv
            #region create formatted message
            try
            {
                if (messageFormat != null)
                {
                    #region generate parameters string list
                    string[] textParametersList = new string[messageParameters.Length];
                    if (messageParameters != null && messageParameters.Length > 0)
                    {
                        textParametersList = new string[messageParameters.Length];
                        for (int i = 0; i < messageParameters.Length; i++)
                        {
                            if (messageParameters[i] != null)
                            {
                                textParametersList[i] = GenerateMessageAccordingToType(messageParameters[i], callerInfo);
                            }
                            else
                            {
                                textParametersList[i] = "";
                                CurrentLogger.Warn(string.Format("{0}:Null is invalid parameter for the formatted string at parameter index {1} while called from {2}", LOCAL_MODULE_NAME, i, callerInfo));
                            }
                        }
                    }
                    #endregion
                    finalMessage = string.Format(callerInfo + ":" + messageFormat, textParametersList.ToArray<object>());
                }
                else
                {
                    #region log warnings
                    if (messageFormat == null)
                        CurrentLogger.Warn(string.Format("{0}:Message format is null while called from {1}", LOCAL_MODULE_NAME, callerInfo));
                    if (messageParameters == null)
                        CurrentLogger.Warn(string.Format("{0}:Message parameters is null while called from {1}", LOCAL_MODULE_NAME, callerInfo));
                    if (messageParameters.Count() == 0)
                        CurrentLogger.Warn(string.Format("{0}:Message parameters count is 0 while called from {1}", LOCAL_MODULE_NAME, callerInfo));
                    #endregion
                }
                #region create logs for passed separate array of objects
                if (logObjectsArr != null)
                {
                    foreach (object currentParameterObject in logObjectsArr)
                    {
                        if (currentParameterObject != null)
                        {
                            finalMessage += "\n logged parameter " + GenerateMessageAccordingToType(currentParameterObject, callerInfo);
                        }
                    }
                }
                #endregion
            }
            //these catches indicate an error in the our logger code or in the calling parameters 
            //so these should be rethrown to protect the caller from silent failure
            catch (FormatException ex)
            {
                CurrentLogger.Error(ex, string.Format("{0}:Error because of string.Format while called from {1}", LOCAL_MODULE_NAME, callerInfo));
                throw;
            }
            catch (Exception ex)
            {
                CurrentLogger.Error(ex, string.Format("{0}:Error inside custom logger while called from {1}", LOCAL_MODULE_NAME, callerInfo));
                throw;
            }
            #endregion
            return finalMessage;
        }

        private static string GenerateMessageAccordingToType(object logObject, string callerInfo)
        {
            string resultLogString = string.Empty;
            try
            {
                //Doing fake bug fix
                if (logObject is ILoggable)
                {
                    resultLogString = ((ILoggable)logObject).ToLogString();
                }
                else
                {
                    resultLogString = logObject.ToString();
                }
            }
            //these catches indicate an error in the our logger code or in the calling parameters 
            //so these should be rethrown to protect the caller from silent failure
            catch (Exception ex)
            {
                CurrentLogger.Error(ex, string.Format("{0}:Error inside custom logger at GenerateMessageAccordingToType while called from {1}", LOCAL_MODULE_NAME, callerInfo));
                throw;
            }
            return resultLogString;
        }

        #endregion

        #region public-methods

        #region Error overloads
        /// <summary>
        /// log using Error log level
        /// </summary>
        /// <param name="ex"> the exception to be logged</param>
        /// <param name="logObjectsArr">array of objects to log their values for custom message user must override toSting() method</param>
        /// <param name="messageFormat">format of the message to be logged</param>
        /// <param name="messageParameters">the parameters to fill the message placeholders</param>
        public static void LogError(string messageFormat, object[] messageParameters = null
            , Exception ex = null, object[] logObjectsArr = null
            , [CallerMemberName]string callerInfo = "")
        {
            string finalMessage = CreateLogMessage(logObjectsArr, messageFormat, callerInfo, messageParameters);
            if (ex == null)
            {
                CurrentLogger.Error(finalMessage);
            }
            else
            {
                CurrentLogger.Error(ex, finalMessage);
            }
        }
        #endregion

        #region debug overloads
        /// <summary>
        /// log using debug log level
        /// </summary>
        /// <param name="ex"> the exception to be logged</param>
        /// <param name="logObjectsArr">array of objects to log their values for custom message user must override toSting() method</param>
        /// <param name="messageFormat">format of the message to be logged</param>
        /// <param name="messageParameters">the parameters to fill the message placeholders</param>
        public static void LogDebug(string messageFormat, object[] messageParameters = null
           , Exception ex = null, object[] logObjectsArr = null
           , [CallerMemberName]string callerInfo = "")
        {
            string finalMessage = CreateLogMessage(logObjectsArr, messageFormat, callerInfo, messageParameters);
            if (ex == null)
            {
                CurrentLogger.Debug(finalMessage);
            }
            else
            {
                CurrentLogger.Debug(ex, finalMessage);
            }
        }
        #endregion

        #region trace overloads

        /// <summary>
        /// log using trace log level
        /// </summary>
        /// <param name="ex"> the exception to be logged</param>
        /// <param name="logObjectsArr">array of objects to log their values for custom message user must override toSting() method</param>
        /// <param name="messageFormat">format of the message to be logged</param>
        /// <param name="messageParameters">the parameters to fill the message placeholders</param>
        public static void LogTrace(string messageFormat, object[] messageParameters = null
           , Exception ex = null, object[] logObjectsArr = null
           , [CallerMemberName]string callerInfo = "")
        {
            string finalMessage = CreateLogMessage(logObjectsArr, messageFormat, callerInfo, messageParameters);
            if (ex == null)
            {
                CurrentLogger.Trace(finalMessage);
            }
            else
            {
                CurrentLogger.Trace(ex, finalMessage);
            }
        }

        #endregion

        #region info overloads
        /// <summary>
        /// log using info log level
        /// </summary>
        /// <param name="ex"> the exception to be logged</param>
        /// <param name="logParaemtersArr">array of objects to log their values for custom message user must override toSting() method</param>
        /// <param name="messageFormat">format of the message to be logged</param>
        /// <param name="messageParameters">the parameters to fill the message placeholders</param>
        public static void LogInfo(string messageFormat, object[] messageParameters = null
           , Exception ex = null, object[] logObjectsArr = null
           , [CallerMemberName]string callerInfo = "")
        {
            string finalMessage = CreateLogMessage(logObjectsArr, messageFormat, callerInfo, messageParameters);
            if (ex == null)
            {
                CurrentLogger.Info(finalMessage);
            }
            else
            {
                CurrentLogger.Info(ex, finalMessage);
            }
        }
        #endregion

        #region warn overloads
        /// <summary>
        /// log using warn log level
        /// </summary>
        /// <param name="ex"> the exception to be logged</param>
        /// <param name="logParaemtersArr">array of objects to log their values for custom message user must override toSting() method</param>
        /// <param name="messageFormat">format of the message to be logged</param>
        /// <param name="messageParameters">the parameters to fill the message placeholders</param>
        public static void LogWarn(string messageFormat, object[] messageParameters = null
           , Exception ex = null, object[] logObjectsArr = null
           , [CallerMemberName]string callerInfo = "")
        {
            string finalMessage = CreateLogMessage(logObjectsArr, messageFormat, callerInfo, messageParameters);
            CurrentLogger.Warn(ex, finalMessage);
        }

        #endregion

        #endregion
    }
}
