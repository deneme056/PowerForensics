﻿using System;
using System.IO;
using System.Text;
using PowerForensics.ExFat;
using PowerForensics.Fat;
using PowerForensics.Ntfs;

namespace PowerForensics.Generic
{
    /// <summary>
    /// 
    /// </summary>
    public class VolumeBootRecord
    {
        #region Enums

        /// <summary>
        /// 
        /// </summary>
        public enum MEDIA_DESCRIPTOR
        {
            /// <summary>
            /// 
            /// </summary>
            FloppyDisk = 0xF0,

            /// <summary>
            /// 
            /// </summary>
            HardDriveDisk = 0xF8
        }

        #endregion Enums

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public MEDIA_DESCRIPTOR MediaDescriptor;

        /// <summary>
        /// 
        /// </summary>
        public ushort BytesPerSector;

        /// <summary>
        /// 
        /// </summary>
        public byte SectorsPerCluster;

        /// <summary>
        /// 
        /// </summary>
        public int BytesPerCluster;

        /// <summary>
        /// 
        /// </summary>
        public ushort ReservedSectors;

        /// <summary>
        /// 
        /// </summary>
        public ushort SectorsPerTrack;

        /// <summary>
        /// 
        /// </summary>
        public ushort NumberOfHeads;

        /// <summary>
        /// 
        /// </summary>
        public uint HiddenSectors;

        /// <summary>
        /// 
        /// </summary>
        public byte[] CodeSection;

        #endregion Properties

        #region Static Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        internal static void checkFooter(byte[] bytes)
        {
            if (BitConverter.ToUInt16(bytes, 0x1FE) != 0xAA55)
            {
                throw new Exception("Invalid VolumeBootRecord Footer.");
            }
        }

        #region Get

        /// <summary>
        /// 
        /// </summary>
        /// <param name="volume"></param>
        /// <returns></returns>
        public static VolumeBootRecord Get(string volume)
        {
            return VolumeBootRecord.Get(GetBytes(volume));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static VolumeBootRecord GetByPath(string path)
        {
            return VolumeBootRecord.Get(GetBytesByPath(path));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="streamToRead"></param>
        /// <returns></returns>
        internal static VolumeBootRecord Get(FileStream streamToRead)
        {
            return VolumeBootRecord.Get(GetBytes(streamToRead));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private static VolumeBootRecord Get(byte[] bytes)
        {
            checkFooter(bytes);

            switch (Helper.GetFileSystemType(bytes))
            {
                case Helper.FILE_SYSTEM_TYPE.EXFAT:
                    //return new ExFatVolumeBootRecord(bytes);
                    return null;
                case Helper.FILE_SYSTEM_TYPE.FAT:
                    return new FatVolumeBootRecord(bytes);
                case Helper.FILE_SYSTEM_TYPE.NTFS:
                    return new NtfsVolumeBootRecord(bytes);
                default:
                    return null;
            }
        }

        #endregion Get

        #region GetBytes

        /// <summary>
        /// 
        /// </summary>
        /// <param name="volume"></param>
        /// <returns></returns>
        public static byte[] GetBytes(string volume)
        {
            return Helper.readDrive(volume, 0x00, 0x200);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="streamToRead"></param>
        /// <returns></returns>
        private static byte[] GetBytes(FileStream streamToRead)
        {
            return Helper.readDrive(streamToRead, 0x00, 0x200);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static byte[] GetBytesByPath(string path)
        {
            FileRecord record = FileRecord.Get(path, true);
            return record.GetContent();
        }

        #endregion GetBytes

        #endregion Static Methods
    }
}