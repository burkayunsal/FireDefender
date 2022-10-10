using System;
using System.Linq;
using UnityEngine;

namespace SBF.Extentions.Numbers
{

    public static class NumberExtentions
    {
        #region INTEGER

        public static string GetOrderText(this int index, bool withNumber)
        {
            string result = "";
            int a = index % 100;
            if (withNumber)
            {

                if (a > 10 && a < 20)
                {
                    result = string.Format("{0}th", index);
                }
                else
                {
                    a %= 10;
                    switch (a)
                    {
                        case 1:
                            result = string.Format("{0}st", index);
                            break;
                        case 2:
                            result = string.Format("{0}nd", index);
                            break;
                        case 3:
                            result = string.Format("{0}rd", index);
                            break;
                        default:
                            result = string.Format("{0}th", index);
                            break;
                    }
                }
            }

            else
            {
                if (a > 10 && a < 20)
                {
                    result = "TH";
                }
                else
                {
                    a %= 10;
                    switch (a)
                    {
                        case 1:
                            result = "ST";
                            break;
                        case 2:
                            result = "ND";
                            break;
                        case 3:
                            result = "RD";
                            break;
                        default:
                            result = "TH";
                            break;
                    }
                }
            }
            return result;
        }

        public static bool Between(this int x, int min, int max, bool inclusive = false)
        {
            if (inclusive)
            {
                return x <= max && x >= min;
            }
            else
            {
                return x < max && x > min;
            }
        }

        public static int Pow(this int val, int pow)
        {
            var result = 1;
            for (int i = 0; i < pow; i++)
            {
                result *= val;
            }
            return result;
        }

        #endregion

        #region FLOAT

        public static bool Between(this float x, float min, float max, bool inclusive = false)
        {
            if (min > max)
            {
                var tmp = min;
                min = max;
                max = tmp;
            }
            if (inclusive)
            {
                return x <= max && x >= min;
            }
            else
            {
                return x < max && x > min;
            }
        }

        public static float Pow(this float val, int pow)
        {
            var result = 1f;
            for (int i = 0; i < pow; i++)
            {
                result *= val;
            }
            return result;
        }

        public static double Lerp(this float t, double start, double end)
        {
            t = Mathf.Clamp01(t);
            return start + ((end - start) * t);
        }

        public static Vector2 DegreeToDirection(this float deg)
        {
            return new Vector2(Mathf.Cos(deg * Mathf.Deg2Rad), Mathf.Sin(deg * Mathf.Deg2Rad));
        }

        #endregion

        #region DOUBLE

        public static double Pow(this double val, int pow)
        {
            var result = 1d;
            for (int i = 0; i < pow; i++)
            {
                result *= val;
            }
            return result;
        }

        #endregion

        #region ABBRIVATION

        public static string AbbrivateNum(this float num)
        {
            return Mathf.FloorToInt(num).AbbrivateNum();
        }

        public static string AbbrivateNum(this int num)
        {
            if (num < 1000) return "" + num;
            int exp = (int)(Mathf.Log(num) / Mathf.Log(1000));
            return string.Format("{0:0.00}{1}", num / 1000.Pow(exp), CURRENCY_ABBREVATIONS.ElementAt(exp - 1));
        }

        public static string AbbrivateNum(this ulong num)
        {
            if (num < 1000) return "" + num;
            int exp = (int)(Math.Log(num) / Math.Log(1000));
            string numStr = string.Format("{0:0.00}", num / Math.Pow(1000, exp));
            numStr = numStr.Substring(0, 4).Trim('.');
            return string.Format("{0}{1}", numStr, CURRENCY_ABBREVATIONS.ElementAt(exp - 1));
        }

        public static string AbbrivateNum(this double num)
        {
            if (num < 1000) return num.ToString("N0");
            int exp = (int)(Math.Log(num) / Math.Log(1000));
            string numStr = string.Format("{0:0.00}", num / Math.Pow(1000, exp));
            numStr = numStr.Substring(0, 4).Trim('.');
            return string.Format("{0}{1}", numStr, CURRENCY_ABBREVATIONS.ElementAt(exp - 1));
        }

        private static readonly string[] CURRENCY_ABBREVATIONS = { "k", "m", "b", "t", "q", "aa", "ab", "ac", "ad", "ae", "af",
        "ag", "ah", "ai", "aj", "ak", "al", "am", "an", "ao", "ap", "aq", "ar", "as", "at", "au", "av", "aw", "ax", "ay",
        "az", "ba", "bb", "bc", "bd", "be", "bf", "bg", "bh", "bi", "bj", "bk", "bl", "bm", "bn", "bo", "bp", "bq", "br",
        "bs", "bt", "bu", "bv", "bw", "bx", "by", "bz", "ca", "cb", "cc", "cd", "ce", "cf", "cg", "ch", "ci", "cj", "ck",
        "cl", "cm", "cn", "co", "cp", "cq", "cr", "cs", "ct", "cu", "cv", "cw", "cx", "cy", "cz", "da", "db", "dc", "dd",
        "de", "df", "dg", "dh", "di", "dj", "dk", "dl", "dm", "dn", "do", "dp", "dq", "dr", "ds", "dt", "du", "dv", "dw",
        "dx", "dy", "dz", "ea", "eb", "ec", "ed", "ee", "ef", "eg", "eh", "ei", "ej", "ek", "el", "em", "en", "eo", "ep",
        "eq", "er", "es", "et", "eu", "ev", "ew", "ex", "ey", "ez", "fa", "fb", "fc", "fd", "fe", "ff", "fg", "fh", "fi",
        "fj", "fk", "fl", "fm", "fn", "fo", "fp", "fq", "fr", "fs", "ft", "fu", "fv", "fw", "fx", "fy", "fz", "ga", "gb",
        "gc", "gd", "ge", "gf", "gg", "gh", "gi", "gj", "gk", "gl", "gm", "gn", "go", "gp", "gq", "gr", "gs", "gt", "gu",
        "gv", "gw", "gx", "gy", "gz", "ha", "hb", "hc", "hd", "he", "hf", "hg", "hh", "hi", "hj", "hk", "hl", "hm", "hn",
        "ho", "hp", "hq", "hr", "hs", "ht", "hu", "hv", "hw", "hx", "hy", "hz", "ia", "ib", "ic", "id", "ie", "if", "ig",
        "ih", "ii", "ij", "ik", "il", "im", "in", "io", "ip", "iq", "ir", "is", "it", "iu", "iv", "iw", "ix", "iy", "iz",
        "ja", "jb", "jc", "jd", "je", "jf", "jg", "jh", "ji", "jj", "jk", "jl", "jm", "jn", "jo", "jp", "jq", "jr", "js",
        "jt", "ju", "jv", "jw", "jx", "jy", "jz", "ka", "kb", "kc", "kd", "ke", "kf", "kg", "kh", "ki", "kj", "kk", "kl",
        "km", "kn", "ko", "kp", "kq", "kr", "ks", "kt", "ku", "kv", "kw", "kx", "ky", "kz", "la", "lb", "lc", "ld", "le",
        "lf", "lg", "lh", "li", "lj", "lk", "ll", "lm", "ln", "lo", "lp", "lq", "lr", "ls", "lt", "lu", "lv", "lw", "lx",
        "ly", "lz", "ma", "mb", "mc", "md", "me", "mf", "mg", "mh", "mi", "mj", "mk", "ml", "mm", "mn", "mo", "mp", "mq",
        "mr", "ms", "mt", "mu", "mv", "mw", "mx", "my", "mz", "na", "nb", "nc", "nd", "ne", "nf", "ng", "nh", "ni", "nj",
        "nk", "nl", "nm", "nn", "no", "np", "nq", "nr", "ns", "nt", "nu", "nv", "nw", "nx", "ny", "nz", "oa", "ob", "oc",
        "od", "oe", "of", "og", "oh", "oi", "oj", "ok", "ol", "om", "on", "oo", "op", "oq", "or", "os", "ot", "ou", "ov",
        "ow", "ox", "oy", "oz", "pa", "pb", "pc", "pd", "pe", "pf", "pg", "ph", "pi", "pj", "pk", "pl", "pm", "pn", "po",
        "pp", "pq", "pr", "ps", "pt", "pu", "pv", "pw", "px", "py", "pz", "qa", "qb", "qc", "qd", "qe", "qf", "qg", "qh",
        "qi", "qj", "qk", "ql", "qm", "qn", "qo", "qp", "qq", "qr", "qs", "qt", "qu", "qv", "qw", "qx", "qy", "qz", "ra",
        "rb", "rc", "rd", "re", "rf", "rg", "rh", "ri", "rj", "rk", "rl", "rm", "rn", "ro", "rp", "rq", "rr", "rs", "rt",
        "ru", "rv", "rw", "rx", "ry", "rz", "sa", "sb", "sc", "sd", "se", "sf", "sg", "sh", "si", "sj", "sk", "sl", "sm",
        "sn", "so", "sp", "sq", "sr", "ss", "st", "su", "sv", "sw", "sx", "sy", "sz", "ta", "tb", "tc", "td", "te", "tf",
        "tg", "th", "ti", "tj", "tk", "tl", "tm", "tn", "to", "tp", "tq", "tr", "ts", "tt", "tu", "tv", "tw", "tx", "ty",
        "tz", "ua", "ub", "uc", "ud", "ue", "uf", "ug", "uh", "ui", "uj", "uk", "ul", "um", "un", "uo", "up", "uq", "ur",
        "us", "ut", "uu", "uv", "uw", "ux", "uy", "uz", "va", "vb", "vc", "vd", "ve", "vf", "vg", "vh", "vi", "vj", "vk",
        "vl", "vm", "vn", "vo", "vp", "vq", "vr", "vs", "vt", "vu", "vv", "vw", "vx", "vy", "vz", "wa", "wb", "wc", "wd",
        "we", "wf", "wg", "wh", "wi", "wj", "wk", "wl", "wm", "wn", "wo", "wp", "wq", "wr", "ws", "wt", "wu", "wv", "ww",
        "wx", "wy", "wz", "xa", "xb", "xc", "xd", "xe", "xf", "xg", "xh", "xi", "xj", "xk", "xl", "xm", "xn", "xo", "xp",
        "xq", "xr", "xs", "xt", "xu", "xv", "xw", "xx", "xy", "xz", "ya", "yb", "yc", "yd", "ye", "yf", "yg", "yh", "yi",
        "yj", "yk", "yl", "ym", "yn", "yo", "yp", "yq", "yr", "ys", "yt", "yu", "yv", "yw", "yx", "yy", "yz", "za", "zb",
        "zc", "zd", "ze", "zf", "zg", "zh", "zi", "zj", "zk", "zl", "zm", "zn", "zo", "zp", "zq", "zr", "zs", "zt", "zu",
        "zv", "zw", "zx", "zy", "zz" };

        #endregion

    }

}
