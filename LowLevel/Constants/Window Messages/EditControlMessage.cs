namespace Win32.LowLevel
{
    public static class EditControlMessage
    {
        public const ushort GETSEL = 0x00B0;
        public const ushort SETSEL = 0x00B1;
        public const ushort GETRECT = 0x00B2;
        public const ushort SETRECT = 0x00B3;
        public const ushort SETRECTNP = 0x00B4;
        public const ushort SCROLL = 0x00B5;
        public const ushort LINESCROLL = 0x00B6;
        public const ushort SCROLLCARET = 0x00B7;
        public const ushort GETMODIFY = 0x00B8;
        public const ushort SETMODIFY = 0x00B9;
        public const ushort GETLINECOUNT = 0x00BA;
        public const ushort LINEINDEX = 0x00BB;
        public const ushort SETHANDLE = 0x00BC;
        public const ushort GETHANDLE = 0x00BD;
        public const ushort GETTHUMB = 0x00BE;
        public const ushort LINELENGTH = 0x00C1;
        public const ushort REPLACESEL = 0x00C2;
        public const ushort GETLINE = 0x00C4;
        public const ushort LIMITTEXT = 0x00C5;
        public const ushort CANUNDO = 0x00C6;
        public const ushort UNDO = 0x00C7;
        public const ushort FMTLINES = 0x00C8;
        public const ushort LINEFROMCHAR = 0x00C9;
        public const ushort SETTABSTOPS = 0x00CB;
        public const ushort SETPASSWORDCHAR = 0x00CC;
        public const ushort EMPTYUNDOBUFFER = 0x00CD;
        public const ushort GETFIRSTVISIBLELINE = 0x00CE;
        public const ushort SETREADONLY = 0x00CF;
        public const ushort SETWORDBREAKPROC = 0x00D0;
        public const ushort GETWORDBREAKPROC = 0x00D1;
        public const ushort GETPASSWORDCHAR = 0x00D2;
        // if (WINVER >= 0x0400)
        public const ushort SETMARGINS = 0x00D3;
        public const ushort GETMARGINS = 0x00D4;
        /// <summary>
        /// win40 Name change
        /// </summary>
        public const ushort SETLIMITTEXT = LIMITTEXT;
        public const ushort GETLIMITTEXT = 0x00D5;
        public const ushort POSFROMCHAR = 0x00D6;
        public const ushort CHARFROMPOS = 0x00D7;

        // if (WINVER >= 0x0500)
        public const ushort SETIMESTATUS = 0x00D8;
        public const ushort GETIMESTATUS = 0x00D9;

        // IDK?
        public const ushort SETFONT = 0x00C3;
        public const ushort SETWORDBREAK = 0x00CA;
    }
}
