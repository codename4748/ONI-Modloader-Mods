﻿namespace MaterialColor
{
    using JetBrains.Annotations;
    using MaterialColor.Data;
    using MaterialColor.Json;
    using ONI_Common.Data;
    using ONI_Common.Json;
    using System.Collections.Generic;
    using UnityEngine;
    using Logger = ONI_Common.IO.Logger;

    public static class State
    {
        [NotNull]
        private static readonly JsonFileLoader JsonLoader = new JsonFileLoader(new JsonManager(), Logger);

        [NotNull]
        public static readonly List<Color> DefaultTemperatureColors = new List<Color>();

        [NotNull]
        public static readonly List<float> DefaultTemperatures = new List<float>();

        // TODO: load from file instead
        [NotNull]
        public static readonly List<string> TileNames = new List<string>
                                                        {
                                                        "Tile",
                                                        "MeshTile",
                                                        "InsulationTile",
                                                        "GasPermeableMembrane",
                                                        "TilePOI",
                                                        "PlasticTile",
                                                        "MetalTile"
                                                        };

        private static MaterialColorState _configuratorState;

        private static Dictionary<SimHashes, ElementColorInfo> _elementColorInfos;

        private static Logger _logger;

        private static TemperatureOverlayState _temperatureOvelayState;

        private static Dictionary<string, Color32> _typeColorOffsets;

        [NotNull]
        public static MaterialColorState ConfiguratorState
        {
            get
            {
                if (_configuratorState != null)
                {
                    return _configuratorState;
                }

                JsonLoader.TryLoadConfiguratorState(out _configuratorState);

                return _configuratorState;
            }

			//private set => _configuratorState = value;
			private set {
				_configuratorState = value;
			}
		}

        [NotNull]
        public static Dictionary<SimHashes, ElementColorInfo> ElementColorInfos
        {
            get
            {
                if (_elementColorInfos != null)
                {
                    return _elementColorInfos;
                }

                // Dictionary<SimHashes, ElementColorInfo> colorInfos;
                JsonLoader.TryLoadElementColorInfos(out _elementColorInfos);

                return _elementColorInfos;
            }

			//private set => _elementColorInfos = value;
			private set
			{
				_elementColorInfos = value;
			}
		}

        [NotNull]
        public static Logger Logger => _logger ?? (_logger = new ONI_Common.IO.Logger(Paths.MaterialColorLogFileName));

        [NotNull]
        public static TemperatureOverlayState TemperatureOverlayState
        {
            get
            {
                if (_temperatureOvelayState != null)
                {
                    return _temperatureOvelayState;
                }

                JsonLoader.TryLoadTemperatureState(out _temperatureOvelayState);

                return _temperatureOvelayState;
            }

			//private set => _temperatureOvelayState = value;
			private set
			{
				_temperatureOvelayState = value;
			}
		}

        [NotNull]
        public static Dictionary<string, Color32> TypeColorOffsets
        {
            get
            {
                if (_typeColorOffsets != null)
                {
                    return _typeColorOffsets;
                }

                JsonLoader.TryLoadTypeColorOffsets(out _typeColorOffsets);

                return _typeColorOffsets;
            }

			//private set => _typeColorOffsets = value;
			private set
			{
				_typeColorOffsets = value;
			}
		}

        public static bool TryReloadConfiguratorState()
        {
			//if (!JsonLoader.TryLoadConfiguratorState(out MaterialColorState state))
			MaterialColorState state;
			if (!JsonLoader.TryLoadConfiguratorState(out state))
			{
				return false;
            }

            ConfiguratorState = state;

            return true;
        }

        public static bool TryReloadElementColorInfos()
        {
			//if (!JsonLoader.TryLoadElementColorInfos(out Dictionary<SimHashes, ElementColorInfo> colorInfos))
			Dictionary<SimHashes, ElementColorInfo> colorInfos;
			if (!JsonLoader.TryLoadElementColorInfos(out colorInfos))
			{
                return false;
            }

            ElementColorInfos = colorInfos;

            return true;
        }

        public static bool TryReloadTemperatureState()
        {
			//if (!JsonLoader.TryLoadTemperatureState(out TemperatureOverlayState temperatureState))
			TemperatureOverlayState temperatureState;
			if (!JsonLoader.TryLoadTemperatureState(out temperatureState))
			{
                return false;
            }

            TemperatureOverlayState = temperatureState;

            return true;
        }

        public static bool TryReloadTypeColorOffsets()
        {
			//if (!JsonLoader.TryLoadTypeColorOffsets(out Dictionary<string, Color32> colorOffsets))
			Dictionary<string, Color32> colorOffsets;
			if (!JsonLoader.TryLoadTypeColorOffsets(out colorOffsets))
			{
                return false;
            }

            TypeColorOffsets = colorOffsets;

            return true;
        }
    }
}