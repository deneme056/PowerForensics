﻿using System.Management.Automation;
using PowerForensics.Ntfs;

namespace PowerForensics.Cmdlets
{
    #region CopyFileCommand

    /// <summary> 
    /// This class implements the Copy-File cmdlet. 
    /// </summary> 
    [Cmdlet(VerbsCommon.Copy, "ForensicFile")]
    public class CopyFileCommand : PSCmdlet
    {
        #region Parameters

        /// <summary> 
        /// This parameter provides the Name of the Volume
        /// for which the FileRecord object should be
        /// returned.
        /// </summary> 
        [Alias("FullName")]
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "ByPath")]
        public string Path
        {
            get { return path; }
            set { path = value; }
        }
        private string path;

        /// <summary> 
        /// This parameter provides the MFTIndexNumber for the 
        /// FileRecord object that will be returned.
        /// </summary> 
        [Parameter(Mandatory = true, Position = 0, ParameterSetName = "ByIndex")]
        public int Index
        {
            get { return index; }
            set { index = value; }
        }
        private int index;

        /// <summary> 
        /// 
        /// </summary> 
        [Parameter(ParameterSetName = "ByIndex")]
        [ValidatePattern(@"^(\\\\\.\\)?([A-Za-z]:|PHYSICALDRIVE\d)$")]
        public string VolumeName
        {
            get { return volume; }
            set { volume = value; }
        }
        private string volume;

        /// <summary> 
        /// This parameter provides the MFTIndexNumber for the 
        /// FileRecord object that will be returned.
        /// </summary> 
        [Parameter(Mandatory = true, Position = 1)]
        public string Destination
        {
            get { return destination; }
            set { destination = value; }
        }
        private string destination;

        #endregion Parameters

        #region Cmdlet Overrides

        /// <summary> 
        /// 
        /// </summary> 
        protected override void BeginProcessing()
        {
            if (ParameterSetName == "ByIndex")
            {
                Helper.getVolumeName(ref volume);
            }
        }

        /// <summary> 
        /// The ProcessRecord method calls ManagementClass.GetInstances() 
        /// method to iterate through each BindingObject on each system specified.
        /// </summary> 
        protected override void ProcessRecord()
        {
            FileRecord record = null;

            switch (ParameterSetName)
            {
                case "ByPath":
                    record = FileRecord.Get(path, true);
                    break;
                case "ByVolume":
                    record = FileRecord.Get(volume, index, true);
                    break;
            }

            // If user specifies the name of a stream then copy just that stream

            // Else check for multiple DATA attributes

            // If multiple DATA attributes, then copy them all

            // Else copy just the main DATA attribute
            record.CopyFile(destination);
        }

        #endregion Cmdlet Overrides
    }
    #endregion CopyFileCommand
}