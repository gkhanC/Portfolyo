using HypeFire.Library.EditorUtilities.Folder;
using HypeFire.Library.Utilities.Logger;
using UnityEditor;

namespace HypeFire.Tools.FolderEditor
{
#if UNITY_EDITOR

    public static class FolderEditor
    {
        public static void Build(FolderTemplate Template)
        {
        }

        [MenuItem("Folder Manager/Build Default Template")]
        public static void BuildDefaultTemplate()
        {
            if (DefaultFolderTemplate.GlobalAccess is null)
            {
                HFLogger.LogError(DefaultFolderTemplate.GlobalAccess, "is not Exists.");
                HFLogger.LogError(DefaultFolderTemplate.GlobalAccess, ".You can create to Folder Template.");
                return;
            }

            var template = DefaultFolderTemplate.GlobalAccess.templateData;
            foreach (var pack in template.folderPacks)
            {
                foreach (var folderName in pack.folderNames)
                {
                    FolderHelper helper =
                        FolderHelper.GetNewFolderHelper(new FolderHelper.FolderData(folderName), true);

                    if (pack.actionType == FolderPackActionType.Delete)
                    {
                        helper.Delete();
                        continue;
                    }

                    helper.Create();
                }
            }
        }
    }

#endif
}