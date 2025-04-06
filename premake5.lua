HazelRootDirectory = os.getenv("HAZEL_DIR")

include (path.join(HazelRootDirectory, "Hazel", "vendor", "Coral", "Premake", "CSExtensions.lua"))

workspace "The Fall"
	targetdir "build"
	startproject "The Fall"
	configurations { "Debug", "Release", "Dist" }

group "Hazel"

include (path.join(HazelRootDirectory, "Hazel", "vendor", "Coral", "Coral.Managed"))
include (path.join(HazelRootDirectory, "Hazel-ScriptCore"))

group ""

project "The Fall"
	location "Assets/Scripts"
	kind "SharedLib"
	language "C#"
	dotnetframework "net8.0"

	targetname "The Fall"
	targetdir "Assets/Scripts/Binaries"
	objdir "Assets/Scripts/Intermediates"

    propertytags {
        { "AppendTargetFrameworkToOutputPath", "false" },
        { "Nullable", "enable" },
    }

	files {
		"Assets/Scripts/Source/**.cs", 
	}

    links {
        "Hazel-ScriptCore"
    }

	filter "configurations:Debug"
		optimize "Off"
		symbols "Default"

	filter "configurations:Release"
		optimize "On"
		symbols "Default"

	filter "configurations:Dist"
		optimize "Full"
		symbols "Off"
