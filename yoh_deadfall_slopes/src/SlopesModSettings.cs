using System.Linq;
using VoxelTycoon;
using VoxelTycoon.Game.UI;
using VoxelTycoon.Modding;
using static VoxelTycoon.Game.UI.SettingsWindowUpDownItem;

namespace Yoh.VoxelTycoon.Slopes;

public sealed class SlopesModSettings : SettingsMod
{
	public const string RailSlopeLengthMultiplier = nameof(RailSlopeLengthMultiplier);

	protected override void SetDefaults(WorldSettings world) =>
		world.SetInt<SlopesModSettings>(RailSlopeLengthMultiplier, value: 1);

	protected override void SetupSettingsControl(SettingsControl control, WorldSettings world) =>
		control.AddUpDown(
			name: "Rail Slope Length Multiplier",
			description: "Increasing the slope length makes them more realistic and smooth, but requires more space to build them.",
			world.GetInt<SlopesModSettings>(RailSlopeLengthMultiplier).ToString(),
			Enumerable
				.Range(1, 3)
				.Select(
					value => new Option
					{
						Name = value.ToString(),
						Setter = () => world.SetInt<SlopesModSettings>(RailSlopeLengthMultiplier, value)
					})
				.ToArray());
	}
