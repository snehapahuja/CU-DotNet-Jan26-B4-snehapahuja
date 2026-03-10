using ColManagement;

namespace ColManagement
{
    internal class Program
    {
        class CollageManagement
        {
            Dictionary<string, Dictionary<string, int>> studentRecords = new Dictionary<string, Dictionary<string, int>>();
            Dictionary<string, LinkedList<KeyValuePair<string, int>>> studentSubjectsOrder = new Dictionary<string, LinkedList<KeyValuePair<string, int>>>();


            Dictionary<string, Dictionary<string, int>> subjectsRecords = new Dictionary<string, Dictionary<string, int>>();
            Dictionary<string, LinkedList<KeyValuePair<string, int>>> subjectsStudentsOrder = new Dictionary<string, LinkedList<KeyValuePair<string, int>>>();

            public int AddStudent(string studentId, string subject, int marks)
            {
                if (!studentRecords.ContainsKey(studentId))
                {
                    studentRecords[studentId] = new Dictionary<string, int>();
                    studentSubjectsOrder[studentId] = new LinkedList<KeyValuePair<string, int>>();
                }

                if (!subjectsRecords.ContainsKey(subject))
                {
                    subjectsRecords[subject] = new Dictionary<string, int>();
                    subjectsStudentsOrder[subject] = new LinkedList<KeyValuePair<string, int>>();
                }

                if (studentRecords[studentId].ContainsKey(subject))
                {
                    if (marks > studentRecords[studentId][subject])
                    {
                        studentRecords[studentId][subject] = marks;
                        subjectsRecords[subject][studentId] = marks;

                        var node = subjectsStudentsOrder[subject].First;
                        while (node != null)
                        {
                            if (node.Value.Key == studentId)
                            {
                                node.Value = new KeyValuePair<string, int>(studentId, marks);
                                break;
                            }
                            node = node.Next;
                        }
                    }
                }
                else
                {
                    studentRecords[studentId][subject] = marks;
                    subjectsRecords[subject][studentId] = marks;

                    studentSubjectsOrder[studentId].AddLast(new KeyValuePair<string, int>(subject, marks));
                    subjectsStudentsOrder[subject].AddLast(new KeyValuePair<string, int>(studentId, marks));
                }

                return 1;
            }

            public int RemoveStudent(string studentId)
            {
                if (!studentRecords.ContainsKey(studentId))
                    return -1;

                foreach (var subject in studentRecords[studentId].Keys)
                {
                    subjectsRecords[subject].Remove(studentId);

                    var node = subjectsStudentsOrder[subject].First;
                    while (node != null)
                    {
                        if (node.Value.Key == studentId)
                        {
                            subjectsStudentsOrder[subject].Remove(node);
                            break;
                        }
                        node = node.Next;
                    }
                }

                studentRecords.Remove(studentId);
                studentSubjectsOrder.Remove(studentId);

                return 1;
            }

            public string TopStudent(string subject)
            {
                if (!subjectsRecords.ContainsKey(subject) || subjectsRecords[subject].Count == 0)
                    return "";

                int maxMarks = subjectsRecords[subject].Values.Max();

                List<string> result = new List<string>();

                foreach (var entry in subjectsStudentsOrder[subject])
                {
                    if (entry.Value == maxMarks)
                    {
                        result.Add(entry.Key + " " + entry.Value);
                    }
                }

                return string.Join("\n", result);
            }

            public string Result()
            {
                List<string> output = new List<string>();

                foreach (var student in studentRecords)
                {
                    double avg = student.Value.Values.Average();
                    output.Add(student.Key + " " + avg.ToString("F2"));
                }

                return string.Join("\n", output);
            }
        }
      
    
        static void Main(string[] args)
        {
            CollageManagement cm = new CollageManagement();

            while (true)
            {
                string input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                    break;

                var parts = input.Split(' ');

                if (parts[0] == "ADD")
                {
                    cm.AddStudent(parts[1], parts[2], int.Parse(parts[3]));
                }
                else if (parts[0] == "REMOVE")
                {
                    cm.RemoveStudent(parts[1]);
                }
                else if (parts[0] == "TOP")
                {
                    Console.WriteLine(cm.TopStudent(parts[1]));
                }
                else if (parts[0] == "RESULT")
                {
                    Console.WriteLine(cm.Result());
                }
            }
        }
    }
}
