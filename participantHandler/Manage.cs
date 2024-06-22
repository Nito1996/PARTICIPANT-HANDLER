using System.Collections.Generic;

namespace participantHandler
{
    public interface IManage
    {
        public void Add(string name, string lastName);
        public void Remove(int index);
        IList<Person> GetParticipants();
    }

    public class Manage : IManage
    {
        public IList<Person> participants = new List<Person>();
        public void Add(string name, string LastName)
        {
            participants.Add(new Person(name, LastName));
        }

        public void Remove(int index)
        {
            participants.RemoveAt(index);
        }

        public IList<Person> GetParticipants()
        {
            return participants;
        }
    }
}
