// Tinfoil Engine Build Script

using Sharpmake;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TinfoilBuildTool;

using Configuration = Sharpmake.Project.Configuration;

[BuildModule(typeof(TinfoilProject))]
public class ImGui : TinfoilThirdPartyModuleDependency
{
    public ImGui(ReadOnlyModuleRules rules) : base(rules)
    {
    }

    public override void ConfigureBuild(Configuration config, ITarget target)
    {
        base.ConfigureBuild(config, target);

        config.Defines.Add("IMGUI_DEFINE_MATH_OPERATORS");
        config.IncludePaths.Add("[project.SharpmakeCsPath]");

        config.ExportDefines.Add("TF_WITH_IMGUI");

        // C4189: local variable not referenced
        config.Options.Add(new Options.Vc.Compiler.DisableSpecificWarnings("4189"));

        // TODO: It would be nice to include api/platform files base on other modules' settings.
        // Ex. If we set NVRHI to only use d3d12, we want to exclude vulkan and d3d11 examples
        AddModulePrivateDependency<GLFW>(config, target);
        AddModulePrivateDependency<NVRHI>(config, target);

        List<string> platformsToRemove = new List<string>();
        platformsToRemove.AddRange(new string[] { "allegro5", "android", "dx9", "dx10", "glut", "metal", "opengl", "osx", "sdl", "wgpu", "win32" });
        FilterSourceFiles(config, platformsToRemove);
    }

    private void FilterSourceFiles(Configuration config, List<string> platformToRemove)
    {
        config.SourceFilesBuildExclude.AddRange(Directory.EnumerateFiles(Path.Combine(RootDirectory, "examples"), "*", SearchOption.AllDirectories));
        config.SourceFilesBuildExclude.AddRange(Directory.EnumerateFiles(Path.Combine(RootDirectory, "misc"), "*", SearchOption.AllDirectories));

        config.SourceFilesBuildExclude.AddRange(Directory.EnumerateFiles(RootDirectory, "*", SearchOption.AllDirectories).
            Where(file => platformToRemove.Any(platform => Path.GetFileName(file).Contains(platform, StringComparison.OrdinalIgnoreCase))));
    }
}
