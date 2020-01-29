//--------------------------------------//               PowerUI////        For documentation or //    if you have any issues, visit//        powerUI.kulestar.com////    Copyright © 2013 Kulestar Ltd//          www.kulestar.com//--------------------------------------using System;using UnityEngine;using System.Collections;using System.Collections.Generic;using Css.Units;using PowerUI;namespace Css{		/// <summary>	/// Used by CSS media queries to define what 'features' the current media has.	/// The main UI uses either the 'screen', 'handheld' or 'tv' media types when possible (default is 'screen').	/// WorldUI's and FlatWorldUI's use the 'gameworld' media type by default (it derives from 'screen').	/// change that by setting the worldUI.Media property.	/// </summary>		public class MediaType{				/// <summary>The document that this is for (if any).</summary>		public ReflowDocument Document;		/// <summary>This media types features.</summary>		public Dictionary<string,Css.Value> Features=new Dictionary<string,Css.Value>();						protected void Ready(ReflowDocument document){			Document=document;		}				public Css.Value this[string property]{			get{				Css.Value value;				Features.TryGetValue(property,out value);				return value;			}			set{								DecimalUnit du=(value as DecimalUnit);								if(du!=null){										// Numeric - if it's zero, act as null:					if(du.RawValue==0f){						value=null;						du=null;												if(!Features.ContainsKey(property)){							// Don't add it.							return;						}											}									}								// Apply the value:				Features[property]=value;								// Min/max too?				if(value!=null){										if(du!=null){												// Numeric - it can therefore have min/max as well:						Features["min-"+property]=value;						Features["max-"+property]=value;											}									}								if(Document!=null){										// Run the change now:					Changed();									}							}		}				/// <summary>Screen width.</summary>		public virtual int Width{			get{				return UnityEngine.Screen.width;			}			set{				// Width changed!				this["width"].SetRawDecimal(value);				this["aspect-ratio"].SetRawDecimal((float)value / (float)Height);								Changed();			}		}				/// <summary>Screen height.</summary>		public virtual int Height{			get{				return UnityEngine.Screen.height;			}			set{				// Height changed!				this["height"].SetRawDecimal(value);				this["aspect-ratio"].SetRawDecimal((float)Width / (float)value);								Changed();			}		}				/// <summary>Is the screen landscape?</summary>		public virtual bool Landscape{			get{				return PowerUI.ScreenInfo.IsLandscape();			}			set{				// Orientation changed!				(this["orientation"] as Css.Units.TextUnit).RawValue=value?"landscape":"portrait";				Changed();			}		}				/// <summary>The resolution of the device.</summary>		public virtual int Resolution{			get{				return ScreenInfo.Dpi;			}			set{				// Resolution changed (..interesting device!)				this["resolution"].SetRawDecimal(value);				Changed();			}		}				/// <summary>The color depth.</summary>		public virtual int Color{			get{				// 8 bits per pixel.				return 8;			}			set{				// Color changed!				this["color"].SetRawDecimal(value);				Changed();			}		}				/// <summary>The colour index (applies to lookup based devices only).</summary>		public virtual int ColorIndex{			get{				// Not lookup based.				return 0;			}			set{				// Color index changed!				this["color-index"].SetRawDecimal(value);				Changed();			}		}				/// <summary>Monochrome device bit depth.</summary>		public virtual int Monochrome{			get{				// Not a monochrome device.				return 0;			}			set{				// Monochrome changed!				this["monochrome"].SetRawDecimal(value);				Changed();			}		}				/// <summary>The scanline format of a screen (usually a TV). progressive or interlace.</summary>		public virtual string Scan{			get{				// Unknown to us inside Unity.				return null;			}			set{				// Scan changed!				(this["scan"] as Css.Units.TextUnit).RawValue=value;				Changed();			}		}				/// <summary>Is this device grid based?</summary>		public virtual int Grid{			get{				// If it is, it probably can't be running Unity!				return 0;			}			set{				// Grid changed!				this["grid"].SetRawDecimal(value);				Changed();			}		}				/// <summary>Prompts the re-evaluation of media queries.</summary>		public void Changed(){						// For each media rule..			foreach(MediaRule mediaRule in Document.MediaRules){								// Run it now:				mediaRule.Evaluate();							}					}				/// <summary>True if this media type is suitable for the given name. For example		/// a mobile media type returns true for at least 'handheld'. Note that 'all' is handled separately.</summary>		public virtual bool Is(string name){			return false;		}				/// <summary>True if this media type has the given named feature (e.g. "color").</summary>		public bool HasFeature(string name){						// Get the value:			// Note that values of '0' are removed entirely.			return (this[name]!=null);					}			}	}