﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Gean.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class FilterString {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal FilterString() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Gean.Resources.FilterString", typeof(FilterString).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to All Files|*.*.
        /// </summary>
        public static string All {
            get {
                return ResourceManager.GetString("All", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Image Files|*.bmp;*.jpg;*.ico;*.icon;*.png;*.gif;|All Files|*.*.
        /// </summary>
        public static string Image {
            get {
                return ResourceManager.GetString("Image", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Media Files|*.mpg;*.avi;*.wma;*.mov;*.wav;*.mp2;*.mp3|All Files|*.*.
        /// </summary>
        public static string Media {
            get {
                return ResourceManager.GetString("Media", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SimpleSudoku Files (*.simsudo)|*.simsudo|All Files (*.*)|*.*.
        /// </summary>
        public static string SimpleSudoku {
            get {
                return ResourceManager.GetString("SimpleSudoku", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Text Files (*.txt)|*.txt|All Files (*.*)|*.*.
        /// </summary>
        public static string Txt {
            get {
                return ResourceManager.GetString("Txt", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Xml Files (*.xml)|*.xml|All Files (*.*)|*.*.
        /// </summary>
        public static string Xml {
            get {
                return ResourceManager.GetString("Xml", resourceCulture);
            }
        }
    }
}