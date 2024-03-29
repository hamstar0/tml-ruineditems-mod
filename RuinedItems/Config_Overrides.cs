﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Terraria.ModLoader.Config;
using ModLibsCore.Classes.Errors;
using ModLibsCore.Libraries.DotNET.Reflection;
using ModLibsCore.Libraries.Debug;

namespace RuinedItems {
	public partial class RuinedItemsConfig : ModConfig {
		private IDictionary<string, object> Overrides = new ConcurrentDictionary<string, object>();



		////////////////

		public T Get<T>( string propName ) {
			if( !this.Overrides.TryGetValue( propName, out object val ) ) {
				if( !ReflectionLibraries.Get( this, propName, out T myval ) ) {
					throw new ModLibsException( "Invalid property " + propName + " of type " + typeof( T ).Name );
				}
				return myval;
			}

			if( val == null ) {
				return default( T );
			}
			if( val.GetType() != typeof( T ) ) {
				throw new ModLibsException( "Invalid type (" + typeof( T ).Name + ") of property " + propName + "." );
			}
			return (T)val;
		}

		////

		public void SetOverride<T>( string propName, T value ) {
			if( !ReflectionLibraries.Get( this, propName, out T _ ) ) {
				throw new ModLibsException( "Invalid property " + propName + " of type " + typeof( T ).Name );
			}

			if( this.DebugMode ) {
				LogLibraries.LogContext( "Overridding setting "+propName+" with "+value );
			}

			this.Overrides[propName] = value;
		}
	}
}
