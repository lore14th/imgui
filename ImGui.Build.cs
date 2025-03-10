// ImGui Tinfoil build script

using Sharpmake;
using System;
using System.IO;
using System.Collections.Generic;

[Sharpmake.Generate]
public class ImGui : TinfoilProjectBase
{
	public ImGui()
	{
		Name = "ImGui";
		SourceFiles.Add("ImGui.Build.cs");
	}

	[Sharpmake.Configure]
	public void ConfigureAll(Project.Configuration config, TinfoilTarget target)
	{
		config.Output = Configuration.OutputType.Lib;

		config.Options.Add(Options.Vc.Compiler.CppLanguageStandard.CPP17);
		config.Options.Add(Options.Vc.General.WindowsTargetPlatformVersion.Latest);
		config.Options.Add(Options.Vc.Librarian.TreatLibWarningAsErrors.Enable);

        config.IncludePaths.Add("");
		config.Defines.Add("IMGUI_DEFINE_MATH_OPERATORS");
		config.Defines.Add("IMGUI_DISABLE_OBSOLETE_FUNCTIONS");

		ExcludeFolder(config, target, "backends");
		ExcludeFolder(config, target, "examples");
		ExcludeFolder(config, target, "misc");
	}
}
