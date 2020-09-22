using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRB.NodeType.SpeedMotorF
{

    public class TimeMappingSpeed
    {
        public Speed_Time[] s_t_table;

        static public TimeMappingSpeed createTest_1(int max_speed)
        {
            int s0 = 0;
            int s1 = max_speed / 4;
            int s2 = max_speed * 3 / 4;
            Speed_Time[] stt = new Speed_Time[10];
            int i = 0;
            stt[i++].init(s0, 0.5f);
            stt[i++].init(s1, 0.2f);
            stt[i++].init(s2, 0.2f);
            stt[i++].init(s1, 0.5f);
            stt[i++].init(s2, 0.5f);
            stt[i++].init(-s2, 0.5f);
            stt[i++].init(-s1, 0.5f);
            stt[i++].init(-s2, 0.2f);
            stt[i++].init(-s1, 0.2f);
            stt[i++].init(s0, 0.5f);
            return new TimeMappingSpeed(stt);
        }



        public TimeMappingSpeed(Speed_Time[] table)
        {
            s_t_table = table;
            max_time = 0;
            foreach (var d in s_t_table)
            {
                max_time += d.Time_ms;
            }
        }
        float max_time;
        public float Max_time => max_time;
        class Motion
        {
            public float time_end;
            public int current_table;

        }

        public virtual int speed(float time, ref object motion)
        {
            Motion m = motion as Motion;
            if (m == null)
            {
                m = new Motion();
                m.current_table = 0;
                m.time_end = time;
                m.time_end += s_t_table[m.current_table].Time_ms;
                motion = m;
            }
            else
            {
                if (m.time_end < time)
                {
                    m.current_table++;
                    m.time_end += s_t_table[m.current_table].Time_ms;
                }
            }
            return s_t_table[m.current_table].Speed;
        }
    }
    public struct Speed_Time
    {
        public int Speed;
        public float Time_ms;
        public void init(int speed, float time_s)
        {
            this.Speed = speed;
            Time_ms = time_s * 1000.0f;
        }

    }
}
