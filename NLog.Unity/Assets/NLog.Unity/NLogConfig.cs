﻿using NLog;
using UnityEngine;

namespace NLog.Unity {

    [DisallowMultipleComponent]
    public class NLogConfig : MonoBehaviour {
        public LogLevel logLevel;
        public bool catchUnityLogs = true;
        Logger _unityLog;

        void Awake() {
            LoggerFactory.globalLogLevel = LogLevel.Off;
        }

        void OnEnable() {
            LoggerFactory.globalLogLevel = logLevel;
        }

        void OnDisable() {
            LoggerFactory.globalLogLevel = LogLevel.Off;
        }

        void Start() {
            if (catchUnityLogs) {
                _unityLog = LoggerFactory.GetLogger("Unity");
                Application.RegisterLogCallback(onLog);
                Application.RegisterLogCallbackThreaded(onLog);
            }
        }

        void onLog(string condition, string stackTrace, LogType type) {
            if (type == LogType.Log) {
                _unityLog.Debug(condition);
            } else if (type == LogType.Warning) {
                _unityLog.Warn(condition);
            } else {
                _unityLog.Error(condition + "\n" + stackTrace);
            }
        }
    }
}