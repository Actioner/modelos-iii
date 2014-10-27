﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18052
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BE.ModelosIII.Resources {
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
    public class ValidationMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ValidationMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("BE.ModelosIII.Resources.ValidationMessages", typeof(ValidationMessages).Assembly);
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
        ///   Looks up a localized string similar to &apos;{PropertyName}&apos; ya existe, por favor elija otro..
        /// </summary>
        public static string AlreadyExists {
            get {
                return ResourceManager.GetString("AlreadyExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ya existe una reserva para el asiento seleccionado..
        /// </summary>
        public static string AlreadyHasReservation {
            get {
                return ResourceManager.GetString("AlreadyHasReservation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to El usuario ingresado se encuentra inhabilitado en este momento..
        /// </summary>
        public static string EmailDisabled {
            get {
                return ResourceManager.GetString("EmailDisabled", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to El usuario ingresado debe validar su email..
        /// </summary>
        public static string EmailInvalidated {
            get {
                return ResourceManager.GetString("EmailInvalidated", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to El nombre de usuario o contraseña ingresados son incorrectos..
        /// </summary>
        public static string EmailOrPasswordInvalid {
            get {
                return ResourceManager.GetString("EmailOrPasswordInvalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to La reserva no está activa, fue pagada o ha vencido..
        /// </summary>
        public static string InvalidReservationForGateway {
            get {
                return ResourceManager.GetString("InvalidReservationForGateway", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to La contraseña no es válida. Mínimo {MinLength} caracteres..
        /// </summary>
        public static string PasswordLengthInvalid {
            get {
                return ResourceManager.GetString("PasswordLengthInvalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Las contraseñas no coinciden..
        /// </summary>
        public static string PasswordsMatch {
            get {
                return ResourceManager.GetString("PasswordsMatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to La promoción no está disponible en ésta fecha..
        /// </summary>
        public static string PromotionNotAvailable {
            get {
                return ResourceManager.GetString("PromotionNotAvailable", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to La reserva no está activa o ya fue pagada..
        /// </summary>
        public static string ReservationCantBeCancel {
            get {
                return ResourceManager.GetString("ReservationCantBeCancel", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to La función seleccionada ya ocurrió..
        /// </summary>
        public static string ScreeningInThePast {
            get {
                return ResourceManager.GetString("ScreeningInThePast", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ya existe una función en ésta franja horaria.
        /// </summary>
        public static string ScreeningOverlap {
            get {
                return ResourceManager.GetString("ScreeningOverlap", resourceCulture);
            }
        }
    }
}