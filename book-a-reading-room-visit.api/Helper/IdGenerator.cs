using System;
using System.Text;

namespace book_a_reading_room_visit.api.Helper
{
    public static class IdGenerator
    {
        private static Random random = new Random();
        private static int baseLenth = 10;
        private static int checkSumPartLength = 5;
        private static string CODE_OPTIONS = "ABEGZSDFHJKXCVNM";
        private static string TRANSACTION_IDENTIFIER = "RR";

        /// <summary>
        /// Generates 13 char long ("RR" + 10 digits + "{letter}") transaction Id based on order number
        /// </summary>
        /// <param name="orderId">Order ID</param>
        /// <example>RR4905402044Q</example>
        /// <returns>18 char long transaction ID</returns>
        public static string GenerateBookingReference(int orderId)
        {
            if (orderId == default(int))
            {
                return String.Empty;
            }

            StringBuilder sb = new StringBuilder(baseLenth);

            int inputLength = orderId.ToString().Length;

            //calculate padding length
            int paddingSize = baseLenth - inputLength - checkSumPartLength;

            //add order number
            sb.Append(orderId);

            //sum of all digits
            int sumOfOrderDigits = 0;
            int tempId = orderId;

            while (tempId != 0)
            {
                sumOfOrderDigits += tempId % 10;
                tempId /= 10;
            }

            //add random padding
            for (int i = 0; i < paddingSize; i++)
            {
                int paddingNo = random.Next(0, 9);
                sumOfOrderDigits += paddingNo;

                sb.Append(paddingNo);
            }

            //multiply sum by the length of order number
            int multiplResult = sumOfOrderDigits * inputLength;

            //add checksum (order number length and order number sum by length result)
            sb.AppendFormat("{0}{1}", inputLength.ToString("D2"), multiplResult.ToString("D3"));

            //result without check character
            var transNoCheckChar = $"{TRANSACTION_IDENTIFIER}{sb}";

            var reference = $"{transNoCheckChar}{GenerateCheckChar(transNoCheckChar)}";

            reference = reference.Insert(5, "-");
            reference = reference.Insert(9, "-");

            return reference;
        }


        /// <summary>
        /// Generates check letter.
        /// </summary>
        /// <remarks>The same functionality is implemented in ECommerce database (_GetCheckCode_ stored proc)</remarks>
        /// <param name="baseInput"></param>
        /// <returns></returns>
        private static string GenerateCheckChar(string baseInput)
        {
            string result = default(string);
            bool isOdd = true;
            int sumOdd = 0;
            int sumEven = 0;

            foreach (var asciiChar in Encoding.ASCII.GetBytes(baseInput))
            {
                if (isOdd)
                {
                    sumOdd = sumOdd + asciiChar;
                    isOdd = false;
                }
                else
                {
                    sumEven = sumEven + asciiChar;
                    isOdd = true;
                }
            }

            result = CODE_OPTIONS.Substring(((sumOdd % 4) * 4 + (sumEven % 4)), 1);

            return result;
        }
    }
}
