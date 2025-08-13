namespace DI.NCFrameWork
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Globalization;

    /// <summary>
    /// �a����������t
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
        /// �a��̔N����������������擾����
        /// </summary>
        public string EraText
        {
            get { return _eraText; }
        }

        /// <summary>
        /// �a��̔N�����擾����
        /// </summary>
        public int Era
        {
            get { return _era; }
        }

        /// <summary>
        /// �a��̔N���擾����
        /// </summary>
        public int Year
        {
            get { return _year; }
        }

        /// <summary>
        /// �����擾����
        /// </summary>
        public int Month
        {
            get { return _month; }
        }

        /// <summary>
        /// �����擾����
        /// </summary>
        public int Day
        {
            get { return _day; }
        }

        /// <summary>
        /// �����擾����
        /// </summary>
        public int Hour
        {
            get { return _hour; }
        }

        /// <summary>
        /// �����擾����
        /// </summary>
        public int Minute
        {
            get { return _minute; }
        }

        /// <summary>
        /// �b���擾����
        /// </summary>
        public int Second
        {
            get { return _second; }
        }

        /// <summary>
        /// �~���b���擾����
        /// </summary>
        public int Millisecond
        {
            get { return _millisecond; }
        }

        /// <summary>
        /// ���t�΂���a����t��Ԃ�
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
        /// �a���\�������`���̓��t�������Ԃ�
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Format("{0}{1:0#}�N{2:0#}��{3:0#}��", _eraText, _year, _month, _day);
        }

        /// <summary>
        /// �a���\�������`���̓����������Ԃ�
        /// </summary>
        /// <returns></returns>
        public string ToDateTimeString()
        {
            return String.Format("{0}{1:0#}�N{2:0#}��{3:0#}��{4:0#}��{5:0#}��{6:0#}�b", 
                _eraText, _year, _month, _day, _hour, _minute, _second);
        }
    }
}
