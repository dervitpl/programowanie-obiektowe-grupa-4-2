using System;

namespace lab22
{
    abstract class AbstractMessage
    {
        public string Content { get; init; }
        abstract public void Send();
    }
    class EmailMessage : AbstractMessage
    {
        public string To { get; init; }
        public string From { get; init; }
        public string Subject { get; init; }

        public override void Send()
        {
            Console.WriteLine($"Sending email from {From} to {To} with subject {Subject}");
        }
        
    }
    class MessangerMessage : AbstractMessage
    {
        public override void Send()
        {
            Console.WriteLine($"Sending messanger message with content {Content}");
        }
    }
    class SmsMessage : AbstractMessage
    {
        public string PhoneNumber { get; init; }
        public override void Send()
        {
            Console.WriteLine($"Sending sms to {PhoneNumber} with content {Content}");
        }
    }
    interface IFly
    {
        void Fly();
    }

    interface ISwim
    {
        void Swim();
    }

    interface IFlyAndSwim: IFly, ISwim
    {

    }
    class Program
    {
        static void Main(string[] args)
        {
            string messageType = "email";
            switch (messageType)
            {
                case "email":
                    Console.WriteLine("Wysyłanie emaila");
                    break;
                case "sms":
                    Console.WriteLine("Wysyłanie sms");
                    break;
                case "messanger":
                    Console.WriteLine("Wysyłanie powiadomienia");
                    break;
            }
            AbstractMessage[] messages = new AbstractMessage[3];
            messages[0] = new EmailMessage() { Content = "Hello", To = "adam@op.pl", From = "", Subject = "" };
            messages[1] = new SmsMessage() { Content = "Hello", PhoneNumber = "666999222" };
            messages[2] = new EmailMessage() { Content = "hi", From = "asda@op.com", To = " sda@op.com", Subject = "hi" };
            messages[3] = new MessangerMessage() { Content = "Wiadomość" };
            int mailCounter = 0;
            foreach(var message in messages)
            {
                message.Send();
                if ( message is EmailMessage)
                {
                    mailCounter++;
                }
                EmailMessage email = message as EmailMessage;
                Console.WriteLine(email.Subject);
                mailCounter += email == null ? 0 : 1;
            }
            Console.WriteLine($"Liczba wysłanych emaili: {mailCounter}");
            IFly[] flyingObjects = new IFly[3];
            flyingObjects[0] = new Duck();
            flyingObjects[1] = new Hydroplane();
            ISwim swimming = flyingObjects[0] as ISwim;
            string[] names = {"Adam", "Ewa", "Karol"}
            IAggregate aggregate = new StringAggregate(names);
            aggregate = new SimpleAggregate() { FirstName="Karol", LastName = "Wojtyla"}

            IAggregate aggregate;
            IIterator itrator = aggregate.createIterator();
            while (iterator.HasNext)
            {
                Console.WriteLine(iterator.GetNext());
            }
        }
    }
    public IIterator
    interface IAggregate
    {
        IIterator createIterator();
    }
    interface IIterator
    {
        bool HasNext();
        string GetNext();
    }
    class SimpleAggregate: IAggregate
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
    }
    
    class StringAggregate : IAggregate
    {
        internal string[] names;

        public StringAggregate(string[] names)
        {
            this.names = names;
        }
        public IIterator createIterator()
        {
            throw new NotImplementedException();
        }
    }
    class SimpleIterator : IIterator
    {
        private SimpleAggregate aggregate;
    }
    
    class StringIterator : IIterator
    {
        private StringAggregate aggregate;
    }
    public StringAggregate (StringAggregate aggregate)
    {
        this.aggregate = aggregate;
    }
    public string GetNext (){
        switch (++count)
        {
            case 1:
                return aggregate.FirstName;
        }
    }
    public bool HasNext()
    {
        return Index < AggregateException.names.Lenght;
    }
}
