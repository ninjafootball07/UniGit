﻿using System;
using JetBrains.Annotations;
using LibGit2Sharp;
using UniGit.Utils;
using UnityEditor;
using UnityEngine;

namespace UniGit
{
	public class GitPullWizard : GitWizardBase
	{
		private PullOptions pullOptions;
		private FetchOptions fetchOptions;
		private MergeOptions mergeOptions;

		[SerializeField]
		private bool prune;
		[SerializeField]
		private bool commitOnSuccess;
		[SerializeField]
		private FastForwardStrategy fastForwardStrategy;
		[SerializeField]
		private ConflictMergeType mergeFileFavor;

		protected override void OnEnable()
		{
			base.OnEnable();
			fetchOptions = new FetchOptions() { CredentialsProvider = CredentialsHandler, OnProgress = FetchProgress, OnTransferProgress = GitManager.FetchTransferProgressHandler, Prune = prune, RepositoryOperationCompleted = FetchOperationCompleted, RepositoryOperationStarting = FetchOperationStarting };
			mergeOptions = new MergeOptions() { CommitOnSuccess = commitOnSuccess, OnCheckoutNotify = OnCheckoutNotify, OnCheckoutProgress = OnCheckoutProgress, FastForwardStrategy = fastForwardStrategy, FileConflictStrategy = (CheckoutFileConflictStrategy)mergeFileFavor };
			pullOptions = new PullOptions() {MergeOptions = mergeOptions, FetchOptions = fetchOptions};
		}

		[UsedImplicitly]
		private void Awake()
		{
			position = new Rect(position.x,position.y,position.width,300);
		}

		protected override bool DrawWizardGUI()
		{
			GUILayout.Label(GitGUI.GetTempContent("Fetch Settings:"), "ProjectBrowserHeaderBgMiddle");
			base.DrawWizardGUI();
			prune = EditorGUILayout.Toggle(GitGUI.GetTempContent("Prune", "Prune all unreachable objects from the object database"), prune);
			GUILayout.Label(GitGUI.GetTempContent("Merge Settings:"), "ProjectBrowserHeaderBgMiddle");
			prune = EditorGUILayout.Toggle(GitGUI.GetTempContent("Prune", "Prune all unreachable objects from the object database"), prune);
			commitOnSuccess = EditorGUILayout.Toggle(GitGUI.GetTempContent("Commit on success"), commitOnSuccess);
			fastForwardStrategy = (FastForwardStrategy)EditorGUILayout.EnumPopup(GitGUI.GetTempContent("Fast Forward Strategy"), fastForwardStrategy);
			mergeFileFavor = (ConflictMergeType)EditorGUILayout.EnumPopup(GitGUI.GetTempContent("File Merge Favor"), mergeFileFavor);
			return false;
		}

		[UsedImplicitly]
		private void OnWizardCreate()
		{
			try
			{
				MergeResult mergeResult = GitManager.Repository.Network.Pull(GitManager.Signature, pullOptions);
				OnMergeComplete(mergeResult,"Pull");
			}
			catch (Exception e)
			{
				Debug.LogException(e);
			}
			finally
			{
				EditorUtility.ClearProgressBar();
			}
		}
	}
}