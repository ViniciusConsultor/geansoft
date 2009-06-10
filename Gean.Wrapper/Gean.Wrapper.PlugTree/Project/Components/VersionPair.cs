using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Diagnostics;
using Gean.Wrapper.PlugTree.Exceptions;
using System.Reflection;
using Gean.Framework;

namespace Gean.Wrapper.PlugTree.Components
{
    public class VersionPair : ICloneable
    {
        public Version MinimumVersion { get; private set; }

        public Version MaximumVersion { get; private set; }

        /// <summary>
        /// 必须预先加载
        /// </summary>
        public bool RequirePreload { get; private set; }

        public string Name
        {
            get { return _Name; }
            set
            {
                if (value == null) throw new ArgumentNullException("name");
                if (value.Length == 0) throw new ArgumentException("name cannot be an empty string", "name");
                _Name = value;
            }
        }
        private string _Name;

        public bool Check(Dictionary<string, Version> plugs, out Version versionFound)
        {
            if (plugs.TryGetValue(_Name, out versionFound))
            {
                return CompareVersion(versionFound, MinimumVersion) >= 0
                    && CompareVersion(versionFound, MaximumVersion) <= 0;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Compares two versions and ignores unspecified fields (unlike Version.CompareTo)
        /// </summary>
        /// <returns>-1 if a &lt; b, 0 if a == b, 1 if a &gt; b</returns>
        int CompareVersion(Version a, Version b)
        {
            if (a.Major != b.Major)
                return a.Major > b.Major ? 1 : -1;
            if (a.Minor != b.Minor)
                return a.Minor > b.Minor ? 1 : -1;
            if (a.Build < 0 || b.Build < 0)
                return 0;
            if (a.Build != b.Build)
                return a.Build > b.Build ? 1 : -1;
            if (a.Revision < 0 || b.Revision < 0)
                return 0;
            if (a.Revision != b.Revision)
                return a.Revision > b.Revision ? 1 : -1;
            return 0;
        }

        public static VersionPair Create(Properties properties)
        {
            VersionPair verEx = new VersionPair((string)properties["name"]);
            string version = (string)properties["version"];
            if (!string.IsNullOrEmpty(version))
            {
                string[] versions = version.Split(new char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                Array.Sort(versions);
                if (versions.Length > 1)
                {
                    verEx.MinimumVersion = ParseVersion(versions[0]);
                    verEx.MaximumVersion = ParseVersion(versions[versions.Length - 1]);
                }
                else
                {
                    verEx.MaximumVersion = verEx.MinimumVersion = ParseVersion(version);
                }
            }
            object rtnstr = null;
            if (!properties.TryGetValue("requirepreload", out rtnstr))
            {
                rtnstr = "false";
            }
            verEx.RequirePreload =
                string.Equals((string)rtnstr, "true", StringComparison.OrdinalIgnoreCase);
            return verEx;
        }

        public static Version ParseVersion(string version)
        {
            if (string.IsNullOrEmpty(version))
                return new Version(0, 0, 0, 0);
            if (version.StartsWith("@"))
            {
                if (version == "@WorkbenchCore")
                {
                    if (_EntryVersion == null)
                        _EntryVersion = Assembly.GetCallingAssembly().GetName().Version;
                    return _EntryVersion;
                }
                else
                {
                    string fileName = version.Substring(1);
                    try
                    {
                        FileVersionInfo info = FileVersionInfo.GetVersionInfo(fileName);
                        Version newVersion = new Version(
                            info.FileMajorPart,
                            info.FileMinorPart,
                            info.FileBuildPart,
                            info.FilePrivatePart);
                        return newVersion;
                    }
                    catch
                    {
                        Version newVersion = new Version(0, 0, 0, 0);
                        return newVersion;
                    }
                }
            }
            else
            {
                Version newVersion;
                try
                {
                    newVersion = new Version(version);
                }
                catch
                {
                    newVersion = new Version(0, 0, 0, 0);
                }
                return newVersion;
            }
        }

        public VersionPair(string name) : this(name, new Version(0, 0, 0, 0), new Version(int.MaxValue, int.MaxValue)) { }

        public VersionPair(string name, Version specificVersion) : this(name, specificVersion, specificVersion) { }

        public VersionPair(string name, Version minimumVersion, Version maximumVersion)
        {
            this.Name = name;
            if (minimumVersion == null) throw new ArgumentNullException("minimumVersion");
            if (maximumVersion == null) throw new ArgumentNullException("maximumVersion");

            this.MinimumVersion = minimumVersion;
            this.MaximumVersion = maximumVersion;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is VersionPair)) return false;
            VersionPair b = (VersionPair)obj;
            return _Name == b._Name && MinimumVersion == b.MinimumVersion && MaximumVersion == b.MaximumVersion;
        }

        public override int GetHashCode()
        {
            return _Name.GetHashCode() ^ MinimumVersion.GetHashCode() ^ MaximumVersion.GetHashCode();
        }

        public override string ToString()
        {
            if (MinimumVersion.ToString() == "0.0.0.0")
            {
                if (MaximumVersion.Major == int.MaxValue)
                    return _Name;
                else
                    return _Name + ", version < " + MaximumVersion.ToString();
            }
            else
            {
                if (MaximumVersion.Major == int.MaxValue)
                    return _Name + ", version > " + MinimumVersion.ToString();
                else if (MinimumVersion == MaximumVersion)
                    return _Name + ", version " + MinimumVersion.ToString();
                else
                    return _Name + ", version " + MinimumVersion.ToString() + "-" + MaximumVersion.ToString();
            }
        }

        #region ICloneable 成员

        public VersionPair Clone()
        {
            return new VersionPair(_Name, MinimumVersion, MaximumVersion);
        }

        object ICloneable.Clone()
        {
            return Clone();
        }

        #endregion

        private static Version _EntryVersion;

    }
}
