using Microsoft.VisualStudio.TestTools.UnitTesting;
using NeptunPro.Deserializers;
using NeptunPro.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace NeptunPro.Tests.Integration.Deserializers
{
    [TestClass]
    public class MessagesPageDeserializerTests
    {
        [TestMethod]
        public void Incoming_Method__Should_Return_Empty_Collection_On_Error()
        {
            var actual = MessagesPageDeserializer.InBox("invalid html");

            CollectionAssert.AreEqual(new List<Message>(), actual);
        }

        [TestMethod]
        public void Incoming_Method__Should_Return_Empty_Collection_When_No_InBox_Message()
        {
            string sourceCode = File.ReadAllText(Path.Combine(Constants.ResourceFolder, "InBox_NoMessages.html"));
            var actual = MessagesPageDeserializer.InBox(sourceCode);

            CollectionAssert.AreEqual(new List<Message>(), actual);
        }

        [TestMethod]
        public void Incoming_Method__Should_Return_Message_Collection_When_User_Has_Messages()
        {
            var expected = new List<Message>()
            {
                new Message(439678344, "Dr. Vámossy Zoltán", "Október 17. előadás ZH", new DateTime(2018, 10, 12, 16, 28, 21, DateTimeKind.Utc)),
                new Message(439657170, "Schmuck Balázs", "NIK TO – információk", new DateTime(2018, 10, 12, 13, 05, 00, DateTimeKind.Utc)),
                new Message(439630013, "Kiss Vivien", "Meghívó AUTOMOTIVE kiállításra az OE-n", new DateTime(2018, 10, 12, 10, 44, 38, DateTimeKind.Utc)),
                new Message(439393021, "Tóthné Laufer Edit", "Bevinf ZH beosztás", new DateTime(2018, 10, 10, 10, 51, 50, DateTimeKind.Utc)),
                new Message(439083332, "Dr Vajda István", "vizsga felkészítő", new DateTime(2018, 10, 08, 05, 47, 47, DateTimeKind.Utc)),
                new Message(438673746, "Fröhlich Martin Michel", "Kis ZH eredmények", new DateTime(2018, 10, 05, 13, 55, 22, DateTimeKind.Utc)),
                new Message(438391624, "Vincze Adrienn", "Októberi nyelvi szintfelmérő felhívás", new DateTime(2018, 10, 03, 13, 14, 52, DateTimeKind.Utc)),
                new Message(438192684, "Tóthné Laufer Edit", "Bevinf elmarad", new DateTime(2018, 10, 01, 15, 08, 51, DateTimeKind.Utc)),
                new Message(438029566, "Fröhlich Martin Michel", "Kis ZH eredmények", new DateTime(2018, 09, 28, 18, 40, 25, DateTimeKind.Utc)),
                new Message(437619263, "Fröhlich Martin Michel", "Labor anyagok", new DateTime(2018, 09, 24, 14, 33, 46, DateTimeKind.Utc)),
                new Message(437208972, "Bilicska Csaba", "Erasmus+ pályázat", new DateTime(2018, 09, 21, 10, 44, 49, DateTimeKind.Utc)),
                new Message(437193273, "Bilicska Csaba", "K-MOOC kurzusok", new DateTime(2018, 09, 21, 10, 32, 45, DateTimeKind.Utc)),
                new Message(437190313, "Bilicska Csaba", "Álláshirdetés", new DateTime(2018, 09, 21, 10, 21, 39, DateTimeKind.Utc)),
                new Message(436985312, "Vincze Adrienn", "Álláslehetőség az Informatikai Osztályon", new DateTime(2018, 09, 19, 13, 56, 45, DateTimeKind.Utc)),
                new Message(436509714, "Bilicska Csaba", "MatLab szoftver telepítése és aktiválása", new DateTime(2018, 09, 17, 08, 30, 31, DateTimeKind.Utc)),
                new Message(436489431, "Schmuck Balázs", "Átsorolási szabály – Ideiglenes diákigazolvány igénylés", new DateTime(2018, 09, 17, 07, 20, 00, DateTimeKind.Utc)),
                new Message(436366332, "Bilicska Csaba", "Mateking.hu oktatási segédanyaghoz hozzáférés", new DateTime(2018, 09, 14, 19, 17, 30, DateTimeKind.Utc)),
                new Message(435954694, "Schmuck Balázs", "Duális képzési lehetőség", new DateTime(2018, 09, 11, 10, 10, 28, DateTimeKind.Utc)),
                new Message(435912477, "Füredi Dominik", "Félévnyitó Gólya Party", new DateTime(2018, 09, 11, 08, 12, 47, DateTimeKind.Utc)),
                new Message(435829643, "Vincze Adrienn", "Rendszeres szociális ösztöndíj pályázat", new DateTime(2018, 09, 10, 13, 21, 40, DateTimeKind.Utc))
            };

            string sourceCode = File.ReadAllText(Path.Combine(Constants.ResourceFolder, "InBox_HasMessages.html"));
            var actual = MessagesPageDeserializer.InBox(sourceCode);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Api_Method__Should_Return_Completed_Message()
        {
            var expected = new Message(440387024, "Fröhlich Martin Michel", "4. kis zh eredmények", new DateTime(2018, 10, 19, 19, 39, 41, DateTimeKind.Utc))
            {
                Text = "Kedves Hallgatók!\n" +
                "A negyedik kis zh eredményeit felraktam a moodle rendszerbe, a pdf frissítve lett, valamint a feltöltés helyén látjátok a megjegyzéseket. Akinek kérdése van, e-mailben felteheti. A nagy zh pontos tematikájával a jövő hét folyamán kaptok egy e-mailt. \n" +
                "Kellemes hétvégét, jó gyakorlást kívánok!\n" +
                "Üdv,\n" +
                "M"
            };

            string sourceCode = File.ReadAllText(Path.Combine(Constants.ResourceFolder, "InBox_DetailedMessage.html"));

            var actual = new Message(440387024, "Fröhlich Martin Michel", "4. kis zh eredmények", new DateTime(2018, 10, 19, 19, 39, 41, DateTimeKind.Utc));

            MessagesPageDeserializer.Api(sourceCode, actual);

            Assert.AreEqual(expected, actual);
        }
    }
}
