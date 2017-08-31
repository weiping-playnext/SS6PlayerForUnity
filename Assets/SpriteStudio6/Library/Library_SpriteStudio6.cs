/**
	SpriteStudio6 Player for Unity

	Copyright(C) Web Technology Corp. 
	All rights reserved.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class Library_SpriteStudio6
{
	/* ----------------------------------------------- Enums & Constants */
	#region Enums & Constants
	public enum KindOperationBlend
	{
		NON = -1,

		MIX = 0,
		ADD,
		SUB,
		MUL,

		TERMINATOR,
	}
	public enum KindOperationBlendEffect
	{
		NON = -1,

		MIX = 0,
		ADD,
		ADD2,

		TERMINATOR,
		TERMINATOR_KIND = ADD,
	}
	public enum KindBoundBlend
	{
		NON = 0,
		OVERALL,
		VERTEX
	}
	public enum KindVertex
	{
		LU = 0,	/* Left-Up (TRIANGLE2 & TRIANGLE4) */
		RU,	/* Right-Up (TRIANGLE2 & TRIANGLE4) */
		RD,	/* Right-Down (TRIANGLE2 & TRIANGLE4) */
		LD,	/* Left-Down (TRIANGLE2 & TRIANGLE4) */
		C,	/* Center (TRIANGLE4) */

		TERMINATOR,
		TERMINATOR4 = TERMINATOR,
		TERMINATOR2 = C
	}
	#endregion Enums & Constants

	/* ----------------------------------------------- Classes, Structs & Interfaces */
	#region Classes, Structs & Interfaces
	public static partial class CallBack
	{
		public delegate bool FunctionPlayEnd(Script_SpriteStudio6_Root instanceRoot, GameObject objectControl);
		public delegate bool FunctionPlayEndEffect(Script_SpriteStudio6_RootEffect instanceRoot);
		public delegate bool FunctionControlEndFrame(Script_SpriteStudio6_Root instanceRoot, int indexControlFrame);
		public delegate void FunctionUserData(Script_SpriteStudio6_Root instanceRoot, string nameParts, int indexParts, int indexAnimation, int frameDecode, int frameKeyData, ref Library_SpriteStudio6.Data.Animation.Attribute.UserData userData, bool flagWayBack);
	}

	public static partial class Data
	{
		/* ----------------------------------------------- Classes, Structs & Interfaces */
		#region Classes, Structs & Interfaces
		[System.Serializable]
		public partial class Animation
		{
			/* ----------------------------------------------- Variables & Properties */
			#region Variables & Properties
			public string Name;
			public int FramePerSecond;
			public int CountFrame;

			public Label[] TableLabel;
			public Parts[] TableParts;
			#endregion Variables & Properties

			/* ----------------------------------------------- Functions */
			#region Functions
			public void CleanUp()
			{
				Name = "";
				FramePerSecond = 0;
				CountFrame = 0;

				TableLabel = null;
				TableParts = null;
			}

			public int CountGetLabel()
			{
				return((null == TableLabel) ? 0 : TableLabel.Length);
			}

			public int IndexGetLabel(string name)
			{
				if((true == string.IsNullOrEmpty(name)) || (null == TableLabel))
				{
					return(-1);
				}

				int count;
				count = (int)Label.KindLabelReserved.TERMINATOR;
				for(int i=0; i<count; i++)
				{
					if(name == Label.TableNameLabelReserved[i])
					{
						return((int)Label.KindLabelReserved.INDEX_RESERVED + i);
					}
				}

				count = TableLabel.Length;
				for(int i=0; i<count; i++)
				{
					if(name == TableLabel[i].Name)
					{
						return(i);
					}
				}
				return(-1);
			}

			public int FrameGetLabel(int index)
			{
				if((int)Label.KindLabelReserved.INDEX_RESERVED <= index)
				{	/* Reserved-Index */
					index -= (int)Label.KindLabelReserved.INDEX_RESERVED;
					switch(index)
					{
						case (int)Label.KindLabelReserved.START:
							return(0);

						case (int)Label.KindLabelReserved.END:
							return(CountFrame - 1);

						default:
							break;
					}
					return(-1);
				}

				if((0 > index) || (TableLabel.Length <= index))
				{
					return(-1);
				}
				return(TableLabel[index].Frame);
			}

			public string NameGetLabel(int index)
			{
				if((int)Label.KindLabelReserved.INDEX_RESERVED <= index)
				{	/* Reserved-Index */
					index -= (int)Label.KindLabelReserved.INDEX_RESERVED;
					if((0 > index) || ((int)Label.KindLabelReserved.TERMINATOR <= index))
					{	/* Error */
						return(null);
					}
					return(Label.TableNameLabelReserved[index]);
				}

				if((0 > index) || (TableLabel.Length <= index))
				{
					return(null);
				}
				return(TableLabel[index].Name);
			}

			public int CountGetParts()
			{
				return((null == TableParts) ? 0 : TableParts.Length);
			}

			public int IndexGetParts(int indexParts)
			{
				return(((0 > indexParts) || (TableParts.Length <= indexParts)) ? -1 : indexParts);
			}
			#endregion Functions

			/* ----------------------------------------------- Classes, Structs & Interfaces */
			#region Classes, Structs & Interfaces
			public static partial class Attribute
			{
				/* Part: SpriteStudio6/Library/Data/Animation/Attribute.cs */
			}

			public static partial class PackAttribute
			{
				/* ----------------------------------------------- Functions */
				#region Functions
				private readonly static CapacityContainer CapacityContainerDummy = new CapacityContainer(
					false,		/* Status */
					false,		/* Position *//* Always Compressed */
					false,		/* Rotation *//* Always Compressed */
					false,		/* Scaling *//* Always Compressed */
					false,		/* RateOpacity */
					false,		/* Priority */
					false,		/* PositionAnchor */
					false,		/* SizeForce */
					false,		/* UserData (Trigger) *//* Always Compressed */
					false,		/* Instance (Trigger) *//* Always Compressed */
					false,		/* Effect (Trigger) *//* Always Compressed */
					false,		/* Plain.Cell */
					false,		/* Plain.ColorBlend */
					false,		/* Plain.VertexCorrection */
					false,		/* Plain.OffsetPivot */
					false,		/* Plain.PositionTexture */
					false,		/* Plain.ScalingTexture */
					false,		/* Plain.RotationTexture */
					false,		/* Plain.RadiusCollision *//* Always Compressed */
					false,		/* Fix.IndexCellMap */
					false,		/* Fix.Coordinate */
					false,		/* Fix.ColorBlend */
					false,		/* Fix.UV0 */
					false,		/* Fix.SizeCollision *//* Always Compressed */
					false,		/* Fix.PivotCollision *//* Always Compressed */
					false		/* Fix.RadiusCollision *//* Always Compressed */
				);
				public static CapacityContainer CapacityGet(KindPack pack)
				{
					switch(pack)
					{
						case KindPack.STANDARD_UNCOMPRESSED:
							return(StandardUncompressed.Capacity);

						case KindPack.STANDARD_CPE:
							return(StandardCPE.Capacity);

						case KindPack.CPE_FLYWEIGHT:
							return(CapacityContainerDummy);

						default:
							break;
					}
					return(null);
				}

				public static string IDGetPack(KindPack pack)
				{
					switch(pack)
					{
						case KindPack.STANDARD_UNCOMPRESSED:
							return(StandardUncompressed.ID);

						case KindPack.STANDARD_CPE:
							return(StandardCPE.ID);

						case KindPack.CPE_FLYWEIGHT:
							return("Dummy");

						default:
							break;
					}
					return(null);
				}
				#endregion Functions

				/* ----------------------------------------------- Enums & Constants */
				#region Enums & Constants
				public enum KindPack
				{
					STANDARD_UNCOMPRESSED = 0,	/* Standard-Uncompressed (Plain Array) */
					STANDARD_CPE,	/* Standard-Compressed (Changing-Point Extracting) */
					CPE_FLYWEIGHT,	/* CPE & GoF-Flyweight */

					TERMINATOR,
				}
				#endregion Enums & Constants

				/* ----------------------------------------------- Classes, Structs & Interfaces */
				#region Classes, Structs & Interfaces
				public interface Container<_Type>
					where _Type : struct
				{	/* MEMO: for Runtime */
					/* ----------------------------------------------- Functions */
					#region Functions
					void CleanUp();
					Library_SpriteStudio6.Data.Animation.PackAttribute.KindPack KindGetPack();

					bool ValueGet(ref _Type outValue, ref int outFrameKey, int framePrevious, ref Library_SpriteStudio6.Data.Animation.PackAttribute.ArgumentContainer argument);
					#endregion Functions
				}
				public class Parameter
				{
					/* ----------------------------------------------- Functions */
					#region Functions
					public virtual KindPack KindGetPack()
					{	/* MEMO: Be sure to override in Derived-Class */
						return((KindPack)(-1));
					}
					#endregion Functions
				}

				public struct ArgumentContainer
				{
					/* ----------------------------------------------- Variables & Properties */
					#region Variables & Properties
					public Script_SpriteStudio6_DataAnimation InstanceDataAnimation;
					public int IndexAnimation;
					public int IndexParts;	/* index of Data.Animation.TableParts[] */
					public int Frame;
					public int FrameKeyPrevious;
					#endregion Variables & Properties

					/* ----------------------------------------------- Functions */
					#region Functions
					public ArgumentContainer(Script_SpriteStudio6_DataAnimation instanceDataAnimation, int indexAnimation, int indexParts, int frame, int frameKeyPrevious)
					{
						InstanceDataAnimation = instanceDataAnimation;
						IndexAnimation = indexAnimation;
						IndexParts = indexParts;
						Frame = frame;
						FrameKeyPrevious = frameKeyPrevious;
					}

					public void CleanUp()
					{
						InstanceDataAnimation = null;
						IndexAnimation = -1;
						IndexParts = -1;
						Frame = -1;
						FrameKeyPrevious = -1;
					}
					#endregion Functions
				}

				public class CapacityContainer
				{
					/* ----------------------------------------------- Variables & Properties */
					#region Variables & Properties
					private FlagBit Flags;
					private FlagBitPlain FlagsPlain;
					private FlagBitFix FlagsFix;

					public bool Status
					{
						get
						{
							return(0 != (Flags & FlagBit.STATUS));
						}
					}
					public bool Position
					{
						get
						{
							return(0 != (Flags & FlagBit.POSITION));
						}
					}
					public bool Rotation
					{
						get
						{
							return(0 != (Flags & FlagBit.ROTATION));
						}
					}
					public bool Scaling
					{
						get
						{
							return(0 != (Flags & FlagBit.SCALING));
						}
					}
					public bool RateOpacity
					{
						get
						{
							return(0 != (Flags & FlagBit.RATE_OPACITY));
						}
					}
					public bool Priority
					{
						get
						{
							return(0 != (Flags & FlagBit.PRIORITY));
						}
					}
					public bool PositionAnchor
					{
						get
						{
							return(0 != (Flags & FlagBit.POSITION_ANCHOR));
						}
					}
					public bool SizeForce
					{
						get
						{
							return(0 != (Flags & FlagBit.SIZE_FORCE));
						}
					}
					public bool UserData
					{
						get
						{
							return(0 != (Flags & FlagBit.USER_DATA));
						}
					}
					public bool Instance
					{
						get
						{
							return(0 != (Flags & FlagBit.INSTANCE));
						}
					}
					public bool Effect
					{
						get
						{
							return(0 != (Flags & FlagBit.EFFECT));
						}
					}

					public bool PlainCell
					{
						get
						{
							return(0 != (FlagsPlain & FlagBitPlain.CELL));
						}
					}
					public bool PlainColorBlend
					{
						get
						{
							return(0 != (FlagsPlain & FlagBitPlain.COLOR_BLEND));
						}
					}
					public bool PlainVertexCorrection
					{
						get
						{
							return(0 != (FlagsPlain & FlagBitPlain.VERTEX_CORRECTION));
						}
					}
					public bool PlainOffsetPivot
					{
						get
						{
							return(0 != (FlagsPlain & FlagBitPlain.OFFSET_PIVOT));
						}
					}
					public bool PlainPositionTexture
					{
						get
						{
							return(0 != (FlagsPlain & FlagBitPlain.POSITION_TEXTURE));
						}
					}
					public bool PlainScalingTexture
					{
						get
						{
							return(0 != (FlagsPlain & FlagBitPlain.SCALING_TEXTURE));
						}
					}
					public bool PlainRotationTexture
					{
						get
						{
							return(0 != (FlagsPlain & FlagBitPlain.ROTATION_TEXTURE));
						}
					}
					public bool PlainRadiusCollision
					{
						get
						{
							return(0 != (FlagsPlain & FlagBitPlain.RADIUS_COLLISION));
						}
					}

					public bool FixIndexCellMap
					{
						get
						{
							return(0 != (FlagsFix & FlagBitFix.INDEX_CELL_MAP));
						}
					}
					public bool FixCoordinate
					{
						get
						{
							return(0 != (FlagsFix & FlagBitFix.COORDINATE));
						}
					}
					public bool FixColorBlend
					{
						get
						{
							return(0 != (FlagsFix & FlagBitFix.COLOR_BLEND));
						}
					}
					public bool FixUV0
					{
						get
						{
							return(0 != (FlagsFix & FlagBitFix.UV0));
						}
					}
					public bool FixSizeCollision
					{
						get
						{
							return(0 != (FlagsFix & FlagBitFix.SIZE_COLLISION));
						}
					}
					public bool FixPivotCollision
					{
						get
						{
							return(0 != (FlagsFix & FlagBitFix.PIVOT_COLLISION));
						}
					}
					public bool FixRadiusCollision
					{
						get
						{
							return(0 != (FlagsFix & FlagBitFix.RADIUS_COLLISION));
						}
					}
					#endregion Variables & Properties

					/* ----------------------------------------------- Functions */
					#region Functions
					public CapacityContainer(	bool status,
												bool position,
												bool rotation,
												bool scaling,
												bool rateOpacity,
												bool priority,
												bool positionAnchor,
												bool sizeForce,
												bool userData,
												bool instance,
												bool effect,
												bool plainCell,
												bool plainColorBlend,
												bool plainVertexCorrection,
												bool plainOffsetPivot,
												bool plainPositionTexture,
												bool plainScalingTexture,
												bool plainRotationTexture,
												bool plainRadiusCollision,
												bool fixIndexCellMap,
												bool fixCoordinate,
												bool fixColorBlend,
												bool fixUV0,
												bool fixSizeCollision,
												bool fixPivotCollision,
												bool fixRadiusCollision
											)
					{
						Flags = 0;
						Flags |= (true == status) ? FlagBit.STATUS : (FlagBit)0;
						Flags |= (true == position) ? FlagBit.POSITION : (FlagBit)0;
						Flags |= (true == rotation) ? FlagBit.ROTATION : (FlagBit)0;
						Flags |= (true == scaling) ? FlagBit.SCALING : (FlagBit)0;
						Flags |= (true == rateOpacity) ? FlagBit.RATE_OPACITY : (FlagBit)0;
						Flags |= (true == priority) ? FlagBit.PRIORITY : (FlagBit)0;
						Flags |= (true == positionAnchor) ? FlagBit.POSITION_ANCHOR : (FlagBit)0;
						Flags |= (true == sizeForce) ? FlagBit.SIZE_FORCE : (FlagBit)0;
						Flags |= (true == userData) ? FlagBit.USER_DATA : (FlagBit)0;
						Flags |= (true == instance) ? FlagBit.INSTANCE : (FlagBit)0;
						Flags |= (true == effect) ? FlagBit.EFFECT : (FlagBit)0;

						FlagsPlain = 0;
						FlagsPlain |= (true == plainCell) ? FlagBitPlain.CELL : (FlagBitPlain)0;
						FlagsPlain |= (true == plainColorBlend) ? FlagBitPlain.COLOR_BLEND : (FlagBitPlain)0;
						FlagsPlain |= (true == plainVertexCorrection) ? FlagBitPlain.VERTEX_CORRECTION : (FlagBitPlain)0;
						FlagsPlain |= (true == plainOffsetPivot) ? FlagBitPlain.OFFSET_PIVOT : (FlagBitPlain)0;
						FlagsPlain |= (true == plainPositionTexture) ? FlagBitPlain.POSITION_TEXTURE : (FlagBitPlain)0;
						FlagsPlain |= (true == plainScalingTexture) ? FlagBitPlain.SCALING_TEXTURE : (FlagBitPlain)0;
						FlagsPlain |= (true == plainRotationTexture) ? FlagBitPlain.ROTATION_TEXTURE : (FlagBitPlain)0;
						FlagsPlain |= (true == plainRadiusCollision) ? FlagBitPlain.RADIUS_COLLISION : (FlagBitPlain)0;

						FlagsFix = 0;
						FlagsFix |= (true == fixIndexCellMap) ? FlagBitFix.INDEX_CELL_MAP : (FlagBitFix)0;
						FlagsFix |= (true == fixCoordinate) ? FlagBitFix.COORDINATE : (FlagBitFix)0;
						FlagsFix |= (true == fixColorBlend) ? FlagBitFix.COLOR_BLEND : (FlagBitFix)0;
						FlagsFix |= (true == fixUV0) ? FlagBitFix.UV0 : (FlagBitFix)0;
						FlagsFix |= (true == fixSizeCollision) ? FlagBitFix.SIZE_COLLISION : (FlagBitFix)0;
						FlagsFix |= (true == fixPivotCollision) ? FlagBitFix.PIVOT_COLLISION : (FlagBitFix)0;
						FlagsFix |= (true == fixRadiusCollision) ? FlagBitFix.RADIUS_COLLISION : (FlagBitFix)0;
					}
					#endregion Functions

					/* ----------------------------------------------- Enums & Constants */
					#region Enums & Constants
					[System.Flags]
					private enum FlagBit
					{
						STATUS = 0x00000001,
						POSITION = 0x00000002,
						ROTATION = 0x00000004,
						SCALING = 0x00000008,
						RATE_OPACITY = 0x00000010,
						PRIORITY = 0x00000020,
						POSITION_ANCHOR = 0x00000040,
						SIZE_FORCE = 0x00000080,
						USER_DATA = 0x00000100,
						INSTANCE = 0x00000200,
						EFFECT = 0x00000400,
					}

					[System.Flags]
					private enum FlagBitPlain
					{
						CELL = 0x00000001,
						COLOR_BLEND = 0x00000002,
						VERTEX_CORRECTION = 0x00000004,
						OFFSET_PIVOT = 0x00000008,
						POSITION_TEXTURE = 0x00000010,
						SCALING_TEXTURE = 0x00000020,
						ROTATION_TEXTURE = 0x00000040,
						RADIUS_COLLISION = 0x00000080,
					}

					[System.Flags]
					private enum FlagBitFix
					{
						INDEX_CELL_MAP = 0x00000001,
						COORDINATE = 0x00000002,
						COLOR_BLEND = 0x00000004,
						UV0 = 0x00000008,
						SIZE_COLLISION = 0x00000010,
						PIVOT_COLLISION = 0x00000020,
						RADIUS_COLLISION = 0x00000040,
					}
					#endregion Enums & Constants
				}
				#endregion Classes, Structs & Interfaces

				/* Implementation: SpriteStudio6/Library/Data/Animation/PackAttribute/*.cs */
			}

			[System.Serializable]
			public struct Parts
			{
				/* ----------------------------------------------- Variables & Properties */
				#region Variables & Properties
				public KindFormat Format;
				public FlagBitStatus StatusParts;

				public Library_SpriteStudio6.Data.Animation.PackAttribute.Container<Library_SpriteStudio6.Data.Animation.Attribute.Status> Status;

				public Library_SpriteStudio6.Data.Animation.PackAttribute.Container<Vector3> Position;	/* Always Compressed */
				public Library_SpriteStudio6.Data.Animation.PackAttribute.Container<Vector3> Rotation;	/* Always Compressed */
				public Library_SpriteStudio6.Data.Animation.PackAttribute.Container<Vector2> Scaling;	/* Always Compressed */

				public Library_SpriteStudio6.Data.Animation.PackAttribute.Container<float> RateOpacity;
				public Library_SpriteStudio6.Data.Animation.PackAttribute.Container<float> Priority;

				public Library_SpriteStudio6.Data.Animation.PackAttribute.Container<Vector2> PositionAnchor;	/* Reserved */
				public Library_SpriteStudio6.Data.Animation.PackAttribute.Container<Vector2> SizeForce;

				public Library_SpriteStudio6.Data.Animation.PackAttribute.Container<Library_SpriteStudio6.Data.Animation.Attribute.UserData> UserData;	/* Trigger (Always Compressed) */
				public Library_SpriteStudio6.Data.Animation.PackAttribute.Container<Library_SpriteStudio6.Data.Animation.Attribute.Instance> Instance;	/* Trigger (Always Compressed) */
				public Library_SpriteStudio6.Data.Animation.PackAttribute.Container<Library_SpriteStudio6.Data.Animation.Attribute.Effect> Effect;	/* Trigger (Always Compressed) */

				public Library_SpriteStudio6.Data.Animation.PackAttribute.Container<float> RadiusCollision;	/* for Sphere-Collider *//* Always Compressed */

				public AttributeGroupPlain Plain;
				public AttributeGroupFix Fix;
				#endregion Variables & Properties

				/* ----------------------------------------------- Enums & Constants */
				#region Enums & Constants
				public enum KindFormat
				{	/* ERROR/NON: -1 */
					PLAIN = 0,	/* Data-Format: Plain-Data */
					FIX,	/* Data-Format: Deformation of "Mesh" and "Collider" are Calculated-In-Advance. */
				}

				[System.Flags]
				public enum FlagBitStatus
				{
					UNUSED = 0x40000000,

					HIDE_FORCE = 0x08000000,
					HIDE_FULL = 0x04000000,

					CLEAR = 0x00000000
				}
				#endregion Enums & Constants

				/* ----------------------------------------------- Classes, Structs & Interfaces */
				#region Classes, Structs & Interfaces
				[System.Serializable]
				public class AttributeGroupPlain
				{
					/* ----------------------------------------------- Variables & Properties */
					#region Variables & Properties
					public Library_SpriteStudio6.Data.Animation.PackAttribute.Container<Library_SpriteStudio6.Data.Animation.Attribute.Cell> Cell;	/* Always Compressed */

					public Library_SpriteStudio6.Data.Animation.PackAttribute.Container<Library_SpriteStudio6.Data.Animation.Attribute.ColorBlend> ColorBlend;
					public Library_SpriteStudio6.Data.Animation.PackAttribute.Container<Library_SpriteStudio6.Data.Animation.Attribute.VertexCorrection> VertexCorrection;
					public Library_SpriteStudio6.Data.Animation.PackAttribute.Container<Vector2> OffsetPivot;

					public Library_SpriteStudio6.Data.Animation.PackAttribute.Container<Vector2> PositionTexture;
					public Library_SpriteStudio6.Data.Animation.PackAttribute.Container<Vector2> ScalingTexture;
					public Library_SpriteStudio6.Data.Animation.PackAttribute.Container<float> RotationTexture;
					#endregion Variables & Properties
				}

				[System.Serializable]
				public class AttributeGroupFix
				{
					/* ----------------------------------------------- Variables & Properties */
					#region Variables & Properties
					public Library_SpriteStudio6.Data.Animation.PackAttribute.Container<int> IndexCellMap;
					public Library_SpriteStudio6.Data.Animation.PackAttribute.Container<Library_SpriteStudio6.Data.Animation.Attribute.CoordinateFix> Coordinate;
					public Library_SpriteStudio6.Data.Animation.PackAttribute.Container<Library_SpriteStudio6.Data.Animation.Attribute.ColorBlendFix> ColorBlend;
					public Library_SpriteStudio6.Data.Animation.PackAttribute.Container<Library_SpriteStudio6.Data.Animation.Attribute.UVFix> UV0;

					public Library_SpriteStudio6.Data.Animation.PackAttribute.Container<Vector2> SizeCollision;	/* for Box-Collider *//* Always Compressed */
					public Library_SpriteStudio6.Data.Animation.PackAttribute.Container<Vector2> PivotCollision;	/* for Box-Collider *//* Always Compressed */
					#endregion Variables & Properties
				}
				#endregion Classes, Structs & Interfaces
			}

			[System.Serializable]
			public struct Label
			{
				/* ----------------------------------------------- Variables & Properties */
				#region Variables & Properties
				public string Name;
				public int Frame;
				#endregion Variables & Properties

				/* ----------------------------------------------- Functions */
				#region Functions
				public void CleanUp()
				{
					Name = "";
					Frame = -1;
				}

				public static int NameCheckReserved(string name)
				{
					if(false == string.IsNullOrEmpty(name))
					{
						for(int i=0; i<(int)KindLabelReserved.TERMINATOR; i++)
						{
							if(name == TableNameLabelReserved[i])
							{
								return(i);
							}
						}
					}
					return(-1);
				}
				#endregion Functions

				/* ----------------------------------------------- Enums & Constants */
				#region Enums & Constants
				public enum KindLabelReserved
				{
					START = 0,	/* "_start" *//* START + INDEX_RESERVED */
					END,	/* "_end" *//* END + INDEX_RESERVED */

					TERMINATOR,
					INDEX_RESERVED = 0x10000000,
				}

				public readonly static string[] TableNameLabelReserved = new string[(int)KindLabelReserved.TERMINATOR]
				{
					"_start",
					"_end",
				};
				#endregion Enums & Constants
			}
			#endregion Classes, Structs & Interfaces
		}

		public static partial class Effect
		{
			/* ----------------------------------------------- Classes, Structs & Interfaces */
			#region Classes, Structs & Interfaces
			[System.Serializable]
			public partial struct Emitter
			{
				/* ----------------------------------------------- Variables & Properties */
				#region Variables & Properties
				public FlagBit FlagData;

				/* Datas for Particle */
				public Library_SpriteStudio6.KindOperationBlendEffect OperationBlendTarget;
				public int IndexCellMap;
				public int IndexCell;

				public RangeFloat Angle;

				public Vector2 GravityDirectional;
				public Vector2 GravityPointPosition;
				public float GravityPointPower;

				public RangeVector2 Position;

				public RangeFloat Rotation;
				public RangeFloat RotationFluctuation;
				public float RotationFluctuationRate;
				public float RotationFluctuationRateTime;

				public RangeFloat RateTangentialAcceleration;

				public RangeVector2 ScaleStart;
				public RangeFloat ScaleRateStart;

				public RangeVector2 ScaleEnd;
				public RangeFloat ScaleRateEnd;

				public int Delay;

				public RangeColor ColorVertex;
				public RangeColor ColorVertexFluctuation;

				public float AlphaFadeStart;
				public float AlphaFadeEnd;

				public RangeFloat Speed;
				public RangeFloat SpeedFluctuation;

				public float TurnDirectionFluctuation;

				public long SeedRandom;

				/* Datas for Emitter */
				public int DurationEmitter;
				public int Interval;
				public RangeFloat DurationParticle;

				public float PriorityParticle;
				public int CountParticleMax;
				public int CountParticleEmit;

				public int CountPartsMaximum;	/* Disuse?? */
				public PatternEmit[] TablePatternEmit;
				public int[] TablePatternOffset;
				public long[] TableSeedParticle;
				#endregion Variables & Properties

				/* ----------------------------------------------- Functions */
				#region Functions
				public void CleanUp()
				{
					FlagData = FlagBit.CLEAR;

					OperationBlendTarget = Library_SpriteStudio6.KindOperationBlendEffect.MIX;
					IndexCellMap = -1;
					IndexCell = -1;

					DurationParticle.Main = 0.0f;
					DurationParticle.Sub = 0.0f;

					Angle.Main = 0.0f;
					Angle.Sub = 0.0f;

					GravityDirectional = Vector2.zero;
					GravityPointPosition = Vector2.zero;
					GravityPointPower = 0.0f;

					Position.Main = Vector2.zero;
					Position.Sub = Vector2.zero;

					Rotation.Main = 0.0f;
					Rotation.Sub = 0.0f;

					RotationFluctuation.Main = 0.0f;
					RotationFluctuation.Sub = 0.0f;
					RotationFluctuationRate = 0.0f;
					RotationFluctuationRateTime = 0.0f;

					RateTangentialAcceleration.Main = 0.0f;
					RateTangentialAcceleration.Sub = 0.0f;

					ScaleStart.Main = Vector2.zero;
					ScaleStart.Sub = Vector2.zero;
					ScaleRateStart.Main = 0.0f;
					ScaleRateStart.Sub = 0.0f;

					ScaleEnd.Main = Vector2.zero;
					ScaleEnd.Sub = Vector2.zero;
					ScaleRateEnd.Main = 0.0f;
					ScaleRateEnd.Sub = 0.0f;

					Delay = 0;

					ColorVertex.Main = new Color(1.0f, 1.0f, 1.0f, 1.0f);
					ColorVertex.Sub = new Color(1.0f, 1.0f, 1.0f, 1.0f);
					ColorVertexFluctuation.Main = new Color(1.0f, 1.0f, 1.0f, 1.0f);
					ColorVertexFluctuation.Sub = new Color(1.0f, 1.0f, 1.0f, 1.0f);

					AlphaFadeStart = 0.0f;
					AlphaFadeEnd = 0.0f;

					Speed.Main = 0.0f;
					Speed.Sub = 0.0f;
					SpeedFluctuation.Main = 0.0f;
					SpeedFluctuation.Sub = 0.0f;

					TurnDirectionFluctuation = 0.0f;

					SeedRandom = (int)Constant.SEED_MAGIC;

					DurationEmitter = 15;
					Interval = 1;
					DurationParticle.Main = 15.0f;
					DurationParticle.Sub = 15.0f;

					PriorityParticle = 64.0f;
					CountParticleEmit = 2;
					CountParticleMax = 32;

					CountPartsMaximum = 0;
					TablePatternEmit = null;
					TablePatternOffset = null;
					TableSeedParticle = null;
				}

				public void Duplicate(Emitter original)
				{
					FlagData = original.FlagData;

					OperationBlendTarget = original.OperationBlendTarget;
					IndexCellMap = original.IndexCellMap;
					IndexCell = original.IndexCell;

					DurationParticle = original.DurationParticle;

					Angle = original.Angle;

					GravityDirectional = original.GravityDirectional;
					GravityPointPosition = original.GravityPointPosition;
					GravityPointPower = original.GravityPointPower;

					Position = original.Position;

					Rotation = original.Rotation;
					RotationFluctuation = original.RotationFluctuation;
					RotationFluctuationRate = original.RotationFluctuationRate;
					RotationFluctuationRateTime = original.RotationFluctuationRateTime;

					RateTangentialAcceleration = original.RateTangentialAcceleration;

					ScaleStart = original.ScaleStart;
					ScaleRateStart = original.ScaleRateStart;
					ScaleEnd = original.ScaleEnd;
					ScaleRateEnd = original.ScaleRateEnd;

					Delay = original.Delay;

					ColorVertex = original.ColorVertex;
					ColorVertexFluctuation = original.ColorVertexFluctuation;

					AlphaFadeStart = original.AlphaFadeStart;
					AlphaFadeEnd = original.AlphaFadeEnd;

					Speed = original.Speed;
					SpeedFluctuation = original.SpeedFluctuation;

					TurnDirectionFluctuation = original.TurnDirectionFluctuation;
					SeedRandom = original.SeedRandom;

					DurationEmitter = original.DurationEmitter;
					Interval = original.Interval;
					DurationParticle = original.DurationParticle;

					PriorityParticle = original.PriorityParticle;
					CountParticleMax = original.CountParticleMax;
					CountParticleEmit = original.CountParticleEmit;

					CountPartsMaximum = original.CountPartsMaximum;
					TablePatternEmit = original.TablePatternEmit;
					TablePatternOffset = original.TablePatternOffset;
					TableSeedParticle = original.TableSeedParticle;
				}

				public void TableGetPatternOffset(ref int[] dataTablePatternOffset)
				{
					int countEmitMax = CountParticleMax;
					int countEmit = CountParticleEmit;
					countEmit = (1 > countEmit) ? 1 : countEmit;

					/* Create Offset-Pattern Table */
					/* MEMO: This Table will be solved at Importing. */
					int shot = 0;
					int offset = Delay;
					int count = countEmitMax;
					dataTablePatternOffset = new int[count];
					for(int i=0; i<count; i++)
					{
						if(shot >= countEmit)
						{
							shot = 0;
							offset += Interval;
						}
						dataTablePatternOffset[i] = offset;
						shot++;
					}
				}

				public void TableGetPatternEmit(	ref PatternEmit[] tablePatternEmit,
													ref long[] tableSeedParticle,
													Library_SpriteStudio6.Utility.Random.Generator random,
													uint seedRandom
												)
				{	/* CAUTION!: Obtain "TablePatternOffset" before executing this function. */
					int count;
					int countEmitMax = CountParticleMax;

					List<PatternEmit> listPatternEmit = new List<PatternEmit>();
					listPatternEmit.Clear();

					int countEmit = CountParticleEmit;
					countEmit = (1 > countEmit) ? 1 : countEmit;

					/* Create Emit-Pattern Table */
					/* MEMO: This Table will be solved at Importing (at seedRandom is fixed). */
					random.InitSeed(seedRandom);
					int cycle = (int)(((float)(countEmitMax * Interval) / (float)countEmit) + 0.5f);
					count = countEmitMax * (int)Constant.LIFE_EXTEND_SCALE;
					if((int)Constant.LIFE_EXTEND_MIN > count)
					{
						count = (int)Constant.LIFE_EXTEND_MIN;
					}
					tablePatternEmit = new PatternEmit[count];
					int duration;
					for(int i=0; i<count; i++)
					{
						tablePatternEmit[i] = new PatternEmit();
						tablePatternEmit[i].IndexGenerate = i;
						duration = (int)((float)DurationParticle.Main + random.RandomFloat((float)DurationParticle.Sub));
						tablePatternEmit[i].Duration = duration;
						tablePatternEmit[i].Cycle = (duration > cycle) ? duration : cycle;
					}

					/* Create Random-Seed Table */
					/* MEMO: This Table will be solved at Importing (at seedRandom is fixed). */
					count = countEmitMax * 3;
					tableSeedParticle = new long[count];
					random.InitSeed(seedRandom);
					for(int i=0; i<count; i++)
					{
						tableSeedParticle[i] = (long)((ulong)random.RandomUint32());
					}
				}
				#endregion Functions

				/* ----------------------------------------------- Enums & Constants */
				#region Enums & Constants
				public enum Constant
				{
					SEED_MAGIC = 7573,

					LIFE_EXTEND_SCALE = 8,
					LIFE_EXTEND_MIN = 64,
				}

				[System.Flags]
				public enum FlagBit
				{
					/* for Particle */
					BASIC = 0x00000001,	/* (Reserved) */
					TANGENTIALACCELATION = 0x00000002,
					TURNDIRECTION = 0x00000004,
					SEEDRANDOM = 0x00000008,
					DELAY = 0x00000010,

					POSITION = 0x00000100,
					POSITION_FLUCTUATION = 0x00000200,	/* (Reserved) */
					ROTATION = 0x00000400,
					ROTATION_FLUCTUATION = 0x00000800,
					SCALE_START = 0x00001000,
					SCALE_END = 0x00002000,

					SPEED = 0x00010000,	/* (Reserved) */
					SPEED_FLUCTUATION = 0x00020000,
					GRAVITY_DIRECTION = 0x00040000,
					GRAVITY_POINT = 0x00080000,

					COLORVERTEX = 0x00100000,
					COLORVERTEX_FLUCTUATION = 0x00200000,
					FADEALPHA = 0x00400000,

					/* for Emitter */
					EMIT_INFINITE = 0x01000000,

					/* Mask-Bit and etc. */
					CLEAR = 0x00000000,
					MASK_EMITTER = 0x7f000000,
					MASK_PARTICLE = 0x00ffffff,
					MASK_VALID = 0x7fffffff,
				}
				#endregion Enums & Constants

				/* ----------------------------------------------- Classes, Structs & Interfaces */
				#region Classes, Structs & Interfaces
				[System.Serializable]
				public struct PatternEmit
				{
					/* ----------------------------------------------- Variables & Properties */
					#region Variables & Properties
					public int IndexGenerate;
//					public int Offset;
					public int Duration;
					public int Cycle;
					#endregion Variables & Properties

					/* ----------------------------------------------- Enums & Constants */
					#region Enums & Constants
					public void CleanUp()
					{
						IndexGenerate = -1;
//						Offset = -1;
						Duration = -1;
						Cycle = -1;
					}
					#endregion Enums & Constants
				}

				[System.Serializable]
				public struct RangeFloat
				{
					/* ----------------------------------------------- Variables & Properties */
					#region Variables & Properties
					public float Main;
					public float Sub;
					#endregion Variables & Properties
				}
				[System.Serializable]
				public struct RangeVector2
				{
					/* ----------------------------------------------- Variables & Properties */
					#region Variables & Properties
					public Vector2 Main;
					public Vector2 Sub;
					#endregion Variables & Properties
				}
				[System.Serializable]
				public struct RangeColor
				{
					/* ----------------------------------------------- Variables & Properties */
					#region Variables & Properties
					public Color Main;
					public Color Sub;
					#endregion Variables & Properties
				}
				#endregion Classes, Structs & Interfaces
			}
			#endregion Classes, Structs & Interfaces
		}

		[System.Serializable]
		public partial class CellMap
		{
			/* ----------------------------------------------- Variables & Properties */
			#region Variables & Properties
			public string Name;
			public Vector2 SizeOriginal;
			public Cell[] TableCell;
			#endregion Variables & Properties

			/* ----------------------------------------------- Functions */
			#region Functions
			public void CleanUp()
			{
				Name = "";
				SizeOriginal = Vector2.zero;
				TableCell = null;
			}

			public int CountGetCell()
			{
				return((null != TableCell) ? TableCell.Length : -1);
			}

			public int IndexGetCell(string name)
			{
				if((true == string.IsNullOrEmpty(name)) || (null == TableCell))
				{
					return(-1);
				}

				int count = TableCell.Length;
				for(int i=0; i<count; i++)
				{
					if(name == TableCell[i].Name)
					{
						return(i);
					}
				}
				return(-1);
			}

			public void Duplicate(CellMap original)
			{
				Name = string.Copy(original.Name);
				SizeOriginal = original.SizeOriginal;
				TableCell = original.TableCell;
			}
			#endregion Functions

			/* ----------------------------------------------- Classes, Structs & Interfaces */
			#region Classes, Structs & Interfaces
			[System.Serializable]
			public struct Cell
			{
				/* ----------------------------------------------- Variables & Properties */
				#region Variables & Properties
				public string Name;
				public Rect Rectangle;
				public Vector2 Pivot;
				#endregion Variables & Properties

				/* ----------------------------------------------- Functions */
				#region Functions
				public void CleanUp()
				{
					Name = "";
					Rectangle.x = 0.0f;
					Rectangle.y = 0.0f;
					Rectangle.width = 0.0f;
					Rectangle.height = 0.0f;
					Pivot = Vector2.zero;
				}

				public void Duplicate(Cell original)
				{
					Name = string.Copy(original.Name);
					Rectangle = original.Rectangle;
					Pivot = original.Pivot;
				}
				#endregion Functions
			}
			#endregion Classes, Structs & Interfaces
		}

		public static partial class Texture
		{
			/* ----------------------------------------------- Enums & Constants */
			#region Enums & Constants
			public enum KindWrap
			{
				CLAMP = 0,
				REPEAT,
				MIRROR,	/* (Unsupported) */
			}
			public enum KindFilter
			{
				NEAREST = 0,
				LINEAR,
				BILINEAR,
			}
			#endregion Enums & Constants
		}

		public static partial class Parts
		{
			/* ----------------------------------------------- Classes, Structs & Interfaces */
			#region Classes, Structs & Interfaces
			[System.Serializable]
			public struct Animation
			{
				/* ----------------------------------------------- Variables & Properties */
				#region Variables & Properties
				public string Name;
				public int ID;
				public int IDParent;
				public int[] TableIDChild;
				public KindFeature Feature;
				public KindColorLabel ColorLabel;
				public Library_SpriteStudio6.KindOperationBlend OperationBlendTarget;

				public KindCollision ShapeCollision;
				public float SizeCollisionZ;

				public Object PrefabUnderControl;
				public string NameAnimationUnderControl;
				#endregion Variables & Properties

				/* ----------------------------------------------- Functions */
				#region Functions
				public void CleanUp()
				{
					Name = "";

					ID = -1;
					IDParent = -1;

					Feature = (KindFeature)(-1);
					OperationBlendTarget = Library_SpriteStudio6.KindOperationBlend.NON;
					ColorLabel = KindColorLabel.NON;

					ShapeCollision = KindCollision.NON;
					SizeCollisionZ = 0.0f;

					PrefabUnderControl = null;
					NameAnimationUnderControl = "";
				}
				#endregion Functions

				/* ----------------------------------------------- Enums & Constants */
				#region Enums & Constants
				public enum KindFeature
				{	/* ERROR/NON: -1 */
					ROOT = 0,	/* Root-Parts (Subspecies of "NULL"-Parts) */
					NULL,
					NORMAL_TRIANGLE2,	/* No use Vertex-Collection Sprite-Parts */
					NORMAL_TRIANGLE4,	/* Use Vertex-Collection Sprite-Parts */

					INSTANCE,
					EFFECT,

					TERMINATOR,
					NORMAL = TERMINATOR	/* NORMAL_TRIANGLE2 or NORMAL_TRIANGLE4 *//* only during import */
				}

				public enum KindColorLabel
				{
					NON = 0,
					RED,
					ORANGE,
					YELLOW,
					GREEN,
					BLUE,
					VIOLET,
					GRAY,
				}

				public enum KindCollision
				{
					NON = 0,
					SQUARE,
					AABB,
					CIRCLE,
					CIRCLE_SCALEMINIMUM,
					CIRCLE_SCALEMAXIMUM
				}
				#endregion Enums & Constants
			}

			[System.Serializable]
			public struct Effect
			{
				/* ----------------------------------------------- Variables & Properties */
				#region Variables & Properties
				public string Name;

				public int ID;
				public int IDParent;
				public int[] TableIDChild;

				public KindFeature Feature;	/* Preliminary ... "Root"or"Emitter" */
				public int IndexEmitter;	/* -1 == Not "Emitter" */
				#endregion Variables & Properties

				/* ----------------------------------------------- Functions */
				#region Functions
				public void CleanUp()
				{
					Name = "";

					ID = -1;
					IDParent = -1;
					TableIDChild = null;

					Feature = (KindFeature)(-1);
					IndexEmitter = -1;
				}
				#endregion Functions

				/* ----------------------------------------------- Enums & Constants */
				#region Enums & Constants
				public enum KindFeature
				{	/* ERROR: -1 */
					ROOT = 0,	/* Root-Parts (Subspecies of "Particle"-Parts) */
					EMITTER,	/* Emitter */
					PARTICLE,	/* Particle */

					TERMINATOR
				}
				#endregion Enums & Constants
			}
			#endregion Classes, Structs & Interfaces
		}

		public static partial class Shader
		{
			/* ----------------------------------------------- Enums & Constants */
			#region Enums & Constants
			public readonly static UnityEngine.Shader[] TableSprite = new UnityEngine.Shader[(int)Library_SpriteStudio6.KindOperationBlend.TERMINATOR]
			{
				UnityEngine.Shader.Find("Custom/SpriteStudio6/Mix"),
				UnityEngine.Shader.Find("Custom/SpriteStudio6/Add"),
				UnityEngine.Shader.Find("Custom/SpriteStudio6/Sub"),
				UnityEngine.Shader.Find("Custom/SpriteStudio6/Mul")
			};

			public readonly static UnityEngine.Shader[] TableEffect = new UnityEngine.Shader[(int)Library_SpriteStudio6.KindOperationBlendEffect.TERMINATOR]
			{
				UnityEngine.Shader.Find("Custom/SpriteStudio6/Effect/Mix"),
				UnityEngine.Shader.Find("Custom/SpriteStudio6/Effect/Add"),
				UnityEngine.Shader.Find("Custom/SpriteStudio6/Effect/Add2"),
			};
			#endregion Enums & Constants
		}
		#endregion Classes, Structs & Interfaces
	}

	public static partial class Script
	{
		/* ----------------------------------------------- Classes, Structs & Interfaces */
		#region Classes, Structs & Interfaces
		public class Root : MonoBehaviour
		{
		}
		#endregion Classes, Structs & Interfaces
	}

	public static partial class Control
	{
		/* ----------------------------------------------- Classes, Structs & Interfaces */
		#region Classes, Structs & Interfaces
		public class Frame
		{
		}

		public class Parts
		{
			/* ----------------------------------------------- Classes, Structs & Interfaces */
			#region Classes, Structs & Interfaces
			public class Animation
			{
			}

			public class Effect
			{
			}
			#endregion Classes, Structs & Interfaces
		}

		public class ColorBlend
		{
		}
		#endregion Classes, Structs & Interfaces
	}

	public static partial class BatchDraw
	{
	}

	public static partial class Miscellaneousness
	{
	}

	public static partial class Utility
	{
		/* ----------------------------------------------- Classes, Structs & Interfaces */
		#region Classes, Structs & Interfaces
		public static partial class Interpolation
		{
			/* ----------------------------------------------- Functions */
			#region Functions
			public static float Linear(float start, float end, float point)
			{
				return(((end - start) * point) + start);
			}

			public static float Hermite(float start, float end, float point, float speedStart, float speedEnd)
			{
				float pointPow2 = point * point;
				float pointPow3 = pointPow2 * point;
				return(	(((2.0f * pointPow3) - (3.0f * pointPow2) + 1.0f) * start)
						+ (((3.0f * pointPow2) - (2.0f * pointPow3)) * end)
						+ ((pointPow3 - (2.0f * pointPow2) + point) * (speedStart - start))
						+ ((pointPow3 - pointPow2) * (speedEnd - end))
					);
			}

			public static float Bezier(ref Vector2 start, ref Vector2 end, float point, ref Vector2 vectorStart, ref Vector2 vectorEnd)
			{
				float pointNow = Linear(start.x, end.x, point);
				float pointTemp;

				float areaNow = 0.5f;
				float RangeNow = 0.5f;

				float baseNow;
				float baseNowPow2;
				float baseNowPow3;
				float areaNowPow2;
				for(int i=0; i<8; i++)
				{
					baseNow = 1.0f - areaNow;
					baseNowPow2 = baseNow * baseNow;
					baseNowPow3 = baseNowPow2 * baseNow;
					areaNowPow2 = areaNow * areaNow;
					pointTemp = (baseNowPow3 * start.x)
								+ (3.0f * baseNowPow2 * areaNow * (vectorStart.x + start.x))
								+ (3.0f * baseNow * areaNowPow2 * (vectorEnd.x + end.x))
								+ (areaNow * areaNowPow2 * end.x);
					RangeNow *= 0.5f;
					areaNow += ((pointTemp > pointNow) ? (-RangeNow) : (RangeNow));
				}

				areaNowPow2 = areaNow * areaNow;
				baseNow = 1.0f - areaNow;
				baseNowPow2 = baseNow * baseNow;
				baseNowPow3 = baseNowPow2 * baseNow;
				return(	(baseNowPow3 * start.y)
						+ (3.0f * baseNowPow2 * areaNow * (vectorStart.y + start.y))
						+ (3.0f * baseNow * areaNowPow2 * (vectorEnd.y + end.y))
						+ (areaNow * areaNowPow2 * end.y)
					);
			}

			public static float Accelerate(float start, float end, float point)
			{
				return(((end - start) * (point * point)) + start);
			}

			public static float Decelerate(float start, float end, float point)
			{
				float pointInverse = 1.0f - point;
				float rate = 1.0f - (pointInverse * pointInverse);
				return(((end - start) * rate) + start);
			}

			public static float ValueGetFloat(	Library_SpriteStudio6.Utility.Interpolation.KindFormula formula,
												int frameNow,
												int frameStart,
												float valueStart,
												int frameEnd,
												float valueEnd,
												float curveFrameStart,
												float curveValueStart,
												float curveFrameEnd,
												float curveValueEnd
											)
			{
				if(frameEnd <= frameStart)
				{
					return(valueStart);
				}
				float frameNormalized = ((float)(frameNow - frameStart)) / ((float)(frameEnd - frameStart));
				frameNormalized = Mathf.Clamp01(frameNormalized);

				switch(formula)
				{
					case KindFormula.NON:
						return(valueStart);

					case KindFormula.LINEAR:
						return(Linear(valueStart, valueEnd, frameNormalized));

					case KindFormula.HERMITE:
						return(Hermite(valueStart, valueEnd, frameNormalized, curveValueStart, curveValueEnd));

					case KindFormula.BEZIER:
						{
							Vector2 start = new Vector2((float)frameStart, valueStart);
							Vector2 vectorStart = new Vector2(curveFrameStart, curveValueStart);
							Vector2 end = new Vector2((float)frameEnd, valueEnd);
							Vector2 vectorEnd = new Vector2(curveFrameEnd, curveValueEnd);
							return(Interpolation.Bezier(ref start, ref end, frameNormalized, ref vectorStart, ref vectorEnd));
						}
//						break;	/* Redundant */

					case KindFormula.ACCELERATE:
						return(Accelerate(valueStart, valueEnd, frameNormalized));

					case KindFormula.DECELERATE:
						return(Decelerate(valueStart, valueEnd, frameNormalized));

					default:
						break;
				}
				return(valueStart);	/* Error */
			}
			#endregion Functions

			/* ----------------------------------------------- Enums & Constants */
			#region Enums & Constants
			public enum KindFormula
			{
				NON = 0,
				LINEAR,
				HERMITE,
				BEZIER,
				ACCELERATE,
				DECELERATE,
			}
			#endregion Enums & Constants
		}

		public static partial class Random
		{
			/* ----------------------------------------------- Classes, Structs & Interfaces */
			#region Classes, Structs & Interfaces
			public interface Generator
			{
				/* ----------------------------------------------- Variables & Properties */
				#region Variables & Properties
				uint[] ListSeed
				{
					get;
				}
				#endregion Variables & Properties

				/* ----------------------------------------------- Functions */
				#region Functions
				void InitSeed(uint seed);
				uint RandomUint32();
				double RandomDouble(double valueMax=1.0);
				float RandomFloat(float valueMax=1.0f);
				int RandomN(int valueMax);
				#endregion Functions
			}
			#endregion Classes, Structs & Interfaces
		}
		#endregion Classes, Structs & Interfaces
	}
	#endregion Classes, Structs & Interfaces
}
