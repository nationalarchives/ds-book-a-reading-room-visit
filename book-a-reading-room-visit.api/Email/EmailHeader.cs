
namespace book_a_reading_room_visit.api.Email
{
    public struct EmailHeader
    {
        private string _name;
        private string _value;

        public EmailHeader(string name, string value)
        {
            _name = name;
            _value = value;
        }

        public string Name => _name;

        public string Value => _value;

        public override string ToString()
        {
            return $"{Name}: {Value}";
        }
    }
}