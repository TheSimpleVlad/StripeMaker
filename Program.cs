using System;
using System.Text;
using System.Threading.Tasks;
using BigMath;

namespace ConsoleApp16
{
    class Program
    {
        static Int256 opix = 0;
        static Int256 olin = 0;
        static Int256 pps = -1;
        static Int256 lps = -1;
        static long GradS = 7;
        static bool Second = false;
        static char[] gradient = " .:,;X&@".ToCharArray();
        static bool sig = false;
        static long it = 0;
        static long max = 0;
        static Int256 pix = 0;
        static Int256 lin = 0;
        static bool sigmainvert;
        static bool Editing = false;
        static bool EditToZero = false;
        static bool Invert = false;
        static bool GradientInvert = false;
        static bool Symmetry = false;
        static bool minusing = false;

        static void OnKeyDown(char e)
        {
            if (e == ' ')
            {
                if (!sig)
                {
                    sig = true;
                }
            }
            if (e == '+')
            {
                it++;
            }
            if (e == '-')
            {
                it--;
            }
            if (e == '*')
            {
                max++;
            }
            if (e == '/')
            {
                max--;
            }
            if (e == '=')
            {
                if (Editing)
                {
                    Editing = false;
                }
                else Editing = true;
            }
            if (e == '0')
            {
                if (EditToZero)
                {
                    EditToZero = false;
                }
                else EditToZero = true;
            }
            if (e == '[')
            {
                if (Invert)
                {
                    Invert = false;
                }
                else Invert = true;
            }
            if (e == ']')
            {
                if (GradientInvert)
                {
                    GradientInvert = false;
                }
                else GradientInvert = true;

                if (GradientInvert)
                {
                    gradient = "@#X;,:. ".ToCharArray();
                }
                else
                {
                    gradient = " .:,;X#@".ToCharArray();
                }
            }
            if (e == '-')
            {
                if (sigmainvert)
                {
                    sigmainvert = false;
                }
                else sigmainvert = true;
            }
            if (e == '\'')
            {
                if (Symmetry)
                {
                    Symmetry = false;
                }
                else Symmetry = true;
            }
        }

        async static Task Main()
        {
            iso:
            Console.Title = "Пикселей обработано: " + pix + " Линий: " + lin + " Оффсет: " + it + " Волны: " + Editing + " Предел: " + max + " К нулю: " + EditToZero + " Инверт: " + Invert + " Инверт цвета: " + GradientInvert + " Переворот: " + sigmainvert + " Имба: " + Symmetry + " | PPS (пикс/сек): " + pps + " LPS (линий/сек): " + lps;
            if (Console.KeyAvailable)
            {
                OnKeyDown(Console.ReadKey().KeyChar);
                Console.Clear();
                await Task.Delay(10);
            }
            if (!sig) goto iso;

            Console.OutputEncoding = Encoding.Unicode;
            Console.InputEncoding = Encoding.Unicode;
            Console.SetCursorPosition(0, 0);
            o:
            int h = Console.WindowHeight;
            Console.BufferHeight = Console.WindowHeight + 1;
            Console.BufferWidth = Console.WindowWidth;
            Console.Write("@");
            int cl = Console.CursorLeft;
            int ct = Console.CursorTop;
            Console.SetCursorPosition(0, 0);
            if (cl >= Console.WindowWidth - 1)
            {
                await Task.Delay(1);
                ct++;
                cl = 0;
            }
            Console.SetCursorPosition(cl, ct);
            if (ct < h) goto o;
            else
            {
                Console.SetCursorPosition(0, 0);
            }
            o2:
            h = Console.WindowHeight;
            Console.BufferHeight = Console.WindowHeight + 1;
            Console.BufferWidth = Console.WindowWidth;
            Console.Write(" ");
            cl = Console.CursorLeft;
            ct = Console.CursorTop;
            if (cl >= Console.WindowWidth - 1)
            {
                await Task.Delay(1);
                ct++;
                cl = 0;
            }
            Console.SetCursorPosition(0, 0);
            if (cl == 0)
            {
                await Task.Delay(1);
            }
            Console.SetCursorPosition(cl, ct);

            if (ct < h) goto o2;
            else
            {
                Ticker();
                oi:
                try
                {
                    Console.SetCursorPosition(0, 0);
                    i:
                    for (int i = Console.WindowWidth; i > 0; i--)
                    {
                        Console.Write(gradient[Convert.ToInt32(Math.Round(double.Parse(i.ToString()) / double.Parse(Console.WindowWidth.ToString()) * double.Parse(GradS.ToString())))]);
                        pix++;
                        Console.Title = "Пикселей обработано: " + pix + " Линий: " + lin + " Оффсет: " + it + " Волны: " + Editing + " Предел: " + max + " К нулю: " + EditToZero + " Инверт: " + Invert + " Инверт цвета: " + GradientInvert + " Переворот: " + sigmainvert + " Имба: " + Symmetry + " | PPS (пикс/сек): " + pps + " LPS (линий/сек): " + lps;
                    }
                    if (it >= 0)
                    {
                        long isos = it;
                        ios:
                        try
                        {
                            Console.CursorLeft += int.Parse(isos.ToString());
                        }
                        catch
                        {
                            int tmp = Console.CursorLeft;
                            isos -= Console.WindowWidth - 1;
                            Console.WriteLine();
                            Console.CursorLeft = tmp;
                            goto ios;
                        }
                    }
                    else
                    {
                        long isos = it;
                        ios:
                        try
                        {
                            Console.CursorLeft += int.Parse(isos.ToString());
                        }
                        catch
                        {
                            isos += Console.WindowWidth - 1;
                            Console.CursorTop--;
                            goto ios;
                        }
                    }
                    if (Editing && sigmainvert)
                    {
                        it = -it;
                    }

                    if (Editing && Symmetry)
                    {
                        if (it > max)
                        {
                            minusing = true;
                        }
                        if (it < -max)
                        {
                            minusing = false;
                        }
                        if (minusing)
                        {
                            it -= 1;
                        }
                        else
                        {
                            it += 1;
                        }
                    }
                    else
                    {
                        if (Editing && !Invert)
                        {
                            it++;
                            if (it > max)
                            {
                                if (!EditToZero)
                                {
                                    it = -max;
                                }
                                else it = 0;
                            }
                            it++;
                            if (it < -max)
                            {
                                if (!EditToZero)
                                {
                                    it = max;
                                }
                                else it = 0;
                            }
                        }
                        if (Editing && Invert)
                        {
                            it--;
                            if (EditToZero)
                            {
                                if (it < 0)
                                {
                                    it = max;
                                }
                            }
                            else
                            {
                                if (it < -max)
                                {
                                    it = max;
                                }
                            }
                        }
                    }
                    if (Editing && sigmainvert)
                    {
                        it = -it;
                    }
                    lin++;
                    if (it == 0)
                    {
                        if (Console.CursorLeft == Console.WindowWidth - 1)
                        {
                            Console.Write(" ");
                        }
                    }
                    await Task.Delay(1);
                    if (Console.KeyAvailable)
                    {
                        OnKeyDown(Console.ReadKey(true).KeyChar);
                        await Task.Delay(1);
                    }
                    if (Second)
                    {
                        Second = false;
                    }
                    goto i;
                }
                catch
                {
                    Console.Clear();
                    Console.WriteLine("Произошла перегрузка консоли, и мне придется начать сначала");
                    await Task.Delay(1000);
                    Console.Clear();
                    goto oi;
                }
            }
        }
        static async void Ticker()
        {
            tick:
            if (!Second)
            {
                await Task.Delay(1000);
                Second = true;
                pps = pix - opix;
                opix = pix;
                lps = lin - olin;
                olin = lin;
            }
            await Task.Delay(1);
            goto tick;
        }
    }
}