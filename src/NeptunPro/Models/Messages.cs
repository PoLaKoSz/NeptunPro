using System;
using System.Collections.Generic;

namespace NeptunPro.Models
{
    public class Message
    {
        public int ID { get; }
        public string Sender { get; }
        public string Subject { get; }
        public string Text { get; set; }
        public DateTime Time { get; }



        public Message(int id, string sender, string subject, DateTime time)
        {
            ID = id;
            Sender = sender;
            Subject = subject;
            Text = "";
            Time = time;
        }



        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj.GetType() != typeof(Message))
                return false;

            Message another = (Message)obj;

            if (ID != another.ID)
                return false;

            if (!Sender.Equals(another.Sender))
                return false;

            if (!Subject.Equals(another.Subject))
                return false;

            if (!Text.Equals(another.Text))
                return false;

            if (Time != another.Time)
                return false;

            return true;
        }

        public override int GetHashCode()
        {
            var hashCode = 1939039797;
            hashCode = hashCode * -1521134295 + ID.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Sender);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Subject);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Text);
            hashCode = hashCode * -1521134295 + Time.GetHashCode();
            return hashCode;
        }
    }
}
