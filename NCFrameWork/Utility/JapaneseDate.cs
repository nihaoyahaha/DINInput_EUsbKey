namespace DI.NCFrameWork
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Globalization;

    /// <summary>
    /// ˜a—ï‚ğ¦‚·“ú•t
    /// </summary>
    public class JapaneseDate
    {
        private string _eraText = "";
        private int _era = 1;
        private int _year = 1;
        private int _month = 1;
        private int _day = 1;
        private int _hour = 0;
        private int _minute = 0;
        private int _second = 0;
        private int _millisecond = 0;

        /// <summary>
        /// ˜a—ï‚Ì”N†‚ğ¦‚·•¶š—ñ‚ğæ“¾‚·‚é
        /// </summary>
        public string EraText
        {
            get { return _eraText; }
        }

        /// <summary>
        /// ˜a—ï‚Ì”N†‚ğæ“¾‚·‚é
        /// </summary>
        public int Era
        {
            get { return _era; }
        }

        /// <summary>
        /// ˜a—ï‚Ì”N‚ğæ“¾‚·‚é
        /// </summary>
        public int Year
        {
            get { return _year; }
        }

        /// <summary>
        /// Œ‚ğæ“¾‚·‚é
        /// </summary>
        public int Month
        {
            get { return _month; }
        }

        /// <summary>
        /// “ú‚ğæ“¾‚·‚é
        /// </summary>
        public int Day
        {
            get { return _day; }
        }

        /// <summary>
        /// ‚ğæ“¾‚·‚é
        /// </summary>
        public int Hour
        {
            get { return _hour; }
        }

        /// <summary>
        /// •ª‚ğæ“¾‚·‚é
        /// </summary>
        public int Minute
        {
            get { return _minute; }
        }

        /// <summary>
        /// •b‚ğæ“¾‚·‚é
        /// </summary>
        public int Second
        {
            get { return _second; }
        }

        /// <summary>
        /// ƒ~ƒŠ•b‚ğæ“¾‚·‚é
        /// </summary>
        public int Millisecond
        {
            get { return _millisecond; }
        }

        /// <summary>
        /// “ú•t‘Î‚·‚é˜a—ï“ú•t‚ğ•Ô‚·
        /// </summary>
        /// <param name="dtValue"></param>
        public JapaneseDate(DateTime dtValue)
        {
            try
            {
                CultureInfo ci = new CultureInfo("ja-JP");
                JapaneseCalendar jpClendar = new JapaneseCalendar();
                ci.DateTimeFormat.Calendar = jpClendar;
                _eraText = dtValue.ToString("gg", ci);
                _era = jpClendar.GetEra(dtValue);
                _year = jpClendar.GetYear(dtValue);
                _month = dtValue.Month;
                _day = dtValue.Day;
                _hour = dtValue.Hour;
                _minute = dtValue.Minute;
                _second = dtValue.Second;
                _millisecond = dtValue.Millisecond;
            }
            catch
            {
            }
        }

        /// <summary>
        /// ˜a—ï‚ğ•\‚·’·‚¢Œ`®‚Ì“ú•t•¶š—ñ‚ğ•Ô‚·
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("{0}{1:0#}”N{2:0#}Œ{3:0#}“ú", _eraText, _year, _month, _day);
        }

        /// <summary>
        /// ˜a—ï‚ğ•\‚·’·‚¢Œ`®‚Ì“ú•¶š—ñ‚ğ•Ô‚·
        /// </summary>
        /// <returns></returns>
        public string ToDateTimeString()
        {
            return String.Format("{0}{1:0#}”N{2:0#}Œ{3:0#}“ú{4:0#}{5:0#}•ª{6:0#}•b", 
                _eraText, _year, _month, _day, _hour, _minute, _second);
        }
    }
}
