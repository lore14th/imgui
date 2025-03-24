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
        // C4189: local variable not referenced
        config.Options.Add(new Options.Vc.Compiler.DisableSpecificWarnings("4189"));

        config.AddPrivateDependency<GLFW>(target, DependencySetting.Default);
        config.AddPrivateDependency<NVRHI>(target, DependencySetting.Default);

        ExcludeFolder(config, target, "examples");
		ExcludeFolder(config, target, "misc");

        // Tinfoil needs dx11, dx12, vk and glfw backends
        // TODO: Remove based on the target supported api
        ExculdeFilesBySuffix(config, target, "allegro5");
        ExculdeFilesBySuffix(config, target, "android");
        ExculdeFilesBySuffix(config, target, "dx9");
        ExculdeFilesBySuffix(config, target, "dx10");
        ExculdeFilesBySuffix(config, target, "glut");
        ExculdeFilesBySuffix(config, target, "metal");
        ExculdeFilesBySuffix(config, target, "opengl");
        ExculdeFilesBySuffix(config, target, "osx");
        ExculdeFilesBySuffix(config, target, "sdl");
        ExculdeFilesBySuffix(config, target, "wgpu");
        ExculdeFilesBySuffix(config, target, "win32");

        List<ERenderingAPI> availableAPIs = GetAvailableRenderingAPIs(target);
        if (!availableAPIs.Contains(ERenderingAPI.NV_D3D11))
        {
            ExculdeFilesBySuffix(config, target, "dx11");
        }
        if(!availableAPIs.Contains(ERenderingAPI.NV_D3D12))
        {
            ExculdeFilesBySuffix(config, target, "dx12");
        }
        if(!availableAPIs.Contains(ERenderingAPI.NV_Vulkan))
        {
            ExculdeFilesBySuffix(config, target, "vulkan");
        }
    }
}
