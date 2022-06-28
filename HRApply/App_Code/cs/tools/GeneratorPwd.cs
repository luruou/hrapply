using System;
using System.Collections.Generic;

namespace ApplyPromote
{
    public class GeneratorPwd
    {
        String newPwd = "";

        int digitOfSet = 8;

        int set = 1;

        public void Execute()
        {

            String pwdSet = "";

            newPwd = "";

            System.Random rand = new System.Random();	// for C#

            int num = 0;

            for (int i = 0; i < set; i++)
            {

                if (i > 0)

                newPwd += "-";	//各組英數之間的分隔號

                pwdSet = "";

                while (pwdSet.Length < digitOfSet)
                {

                    // Java 中 pwdSet.Length 改為 pwdSet.length()

                    num = rand.Next(50, 90);	// for C#

                    //num = (int)(Math.random()*(90-50+1))+50;	// for Java

                    if (num > 57 && num < 65)

                        continue;	//排除 58~64 這區間的非英數符號

                    else if (num == 79 || num == 73)

                        continue;	//排除 I 和 O

                    pwdSet += (char)num;	//將數字轉換為字元

                }

                newPwd += pwdSet;

            }

        }

        public string GetPwd(){
            return newPwd.ToLower();
        }

    }
}
