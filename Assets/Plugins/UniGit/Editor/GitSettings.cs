﻿using System;
using UnityEngine;
using System.Collections;

namespace UniGit
{
	public class GitSettings : ScriptableObject
	{
		[Tooltip("Auto stage changes for committing when an asset is modified")]
		public bool AutoStage = true;
		[Tooltip("Auto fetch repository changes when possible. This will tell you about changes to the remote repository without having to pull. This only works with the Credentials Manager.")]
		public bool AutoFetch = true;
		[Tooltip("The Maximum amount of commits show in the Git History Window. Use -1 for infinite commits.")]
		[Delayed]
		public int MaxCommits = 32;
		public ExternalsTypeEnum ExternalsType;
		public string ExternalProgram;
		public string CredentialsManager;
		[Tooltip("The maximum depth at which overlays will be shown in the Project Window. This means that folders at levels higher than this will not be marked as changed. -1 indicates no limit")]
		[Delayed]
		public int ProjectStatusOverlayDepth = 2;
		[Tooltip("Show status for empty folder meta files and auto stage them, if 'Auto stage' option is enabled.")]
		public bool ShowEmptyFolders = false;
		[Tooltip("Should Git status retrieval be multithreaded.")]
		public bool GitStatusMultithreaded = true;
		[Tooltip("Load Gavatars based on the committer's email address.")]
		public bool UseGavatar = true;
		[Tooltip("The maximum height the commit text area can expand to.")]
		public float MaxCommitTextAreaSize = 120;
		[Tooltip("Detect Renames. This will make UniGit detect rename changes of files. Note that this feature is not always working as expected do the the modular updating and how Git itself works.")]
		public bool DetectRenames = true;

		[Flags]
		[SerializeField]
		public enum ExternalsTypeEnum
		{
			Pull = 1 << 0,
			Push = 1 << 1,
			Fetch = 1 << 2,
			Merge = 1 << 3,
			Commit = 1 << 4,
			Switch = 1 << 5,
			Reset = 1 << 6,
			Revert = 1 << 7
		}
	}
}