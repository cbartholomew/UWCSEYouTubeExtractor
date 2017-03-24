﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.36366
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CSEVideoExtraction.Settings {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "12.0.0.0")]
    internal sealed partial class APISettings : global::System.Configuration.ApplicationSettingsBase {
        
        private static APISettings defaultInstance = ((APISettings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new APISettings())));
        
        public static APISettings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("AIzaSyCn2WcoIo_UxzIICdNTEfS32mN9AkshAhw")]
        public string API_KEY {
            get {
                return ((string)(this["API_KEY"]));
            }
            set {
                this["API_KEY"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("channels?id=#CHANNEL_ID#&key=#API_KEY#&part=contentDetails")]
        public string GET_CHANNEL_UPLOAD_ID {
            get {
                return ((string)(this["GET_CHANNEL_UPLOAD_ID"]));
            }
            set {
                this["GET_CHANNEL_UPLOAD_ID"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("playlistItems?playlistId=#UPLOAD_ID#&key=#API_KEY#&part=snippet,contentDetails&ma" +
            "xResults=50&pageToken=#PAGE_TOKEN#")]
        public string GET_PLAYLIST_ITEMS {
            get {
                return ((string)(this["GET_PLAYLIST_ITEMS"]));
            }
            set {
                this["GET_PLAYLIST_ITEMS"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("videos?part=snippet,contentDetails&id=#VIDEO_ID#&key=#API_KEY#")]
        public string GET_VIDEO_DETAILS {
            get {
                return ((string)(this["GET_VIDEO_DETAILS"]));
            }
            set {
                this["GET_VIDEO_DETAILS"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("https://www.googleapis.com/youtube/v3/")]
        public string API_BASE_URL {
            get {
                return ((string)(this["API_BASE_URL"]));
            }
            set {
                this["API_BASE_URL"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("playlists?part=snippet&id=#PLAYLIST_ID#&key=#API_KEY#")]
        public string GET_PLAYLIST_NAME {
            get {
                return ((string)(this["GET_PLAYLIST_NAME"]));
            }
            set {
                this["GET_PLAYLIST_NAME"] = value;
            }
        }
    }
}
