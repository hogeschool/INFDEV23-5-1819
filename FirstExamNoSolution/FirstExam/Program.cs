using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstExam
{
  public class Course
  {
    public string Code { get; set; }
    public string Name { get; set; }
    public int Duration { get; set; }

    public Course(string code, string name, int duration)
    {
      Code = code;
      Name = name;
      Duration = duration;
    }
  }

  public class Assignment
  {
    public string Code { get; set; }
    public int Month { get; set; }
    public int Day { get; set; }
    public string Type { get; set; }

    public Assignment(string code, int month, int day, string type)
    {
      Code = code;
      Month = month;
      Day = day;
      Type = type;
    }
  }

  public class CourseAssignment
  {
    public string CourseCode { get; set; }
    public string AssignmentCode { get; set; }

    public CourseAssignment(string code, string assignmentCode)
    {
      CourseCode = code;
      AssignmentCode = assignmentCode;
    }
  }

  public static class MapReduce
  {
    public static IEnumerable<T2> Map<T1, T2>(this IEnumerable<T1> collection, Func<T1, T2> transformation)
    {
      T2[] result = new T2[collection.Count()];
      for (int i = 0; i < collection.Count(); i++)
      {
        result[i] = transformation(collection.ElementAt(i));
      }
      return result;
    }

    public static T2 Reduce<T1, T2>(this IEnumerable<T1> collection, T2 init, Func<T2, T1, T2> operation)
    {
      T2 result = init;
      for (int i = 0; i < collection.Count(); i++)
      {
        //TODO: question 1
      }
      return result;
    }

    public static IEnumerable<Tuple<T1, T2>> Join<T1, T2>(this IEnumerable<T1> table1, IEnumerable<T2> table2, Func<T1, T2, bool> condition)
    {
      return
        Reduce(table1, new List<Tuple<T1, T2>>(),
          (queryResult, x) =>
          {
            List<Tuple<T1, T2>> combination =
              Reduce(table2, new List<Tuple<T1, T2>>(),
                      (c, y) =>
                      {
                        //TODO: question 2
                      });
            queryResult.AddRange(combination);
            return queryResult;
          });
    }
  }

  class Program
  {
    public static Course[] CourseTable = new Course[]
    {
      new Course("INFDEV03-5", "Advanced Databases & NoSQL", 16),
      new Course("INFDEV036A", "Algorithms & Data Structures", 16),
      new Course("INFDEV02-2", "Development 2 - Functions and Recursion",5)
    };

    public static Assignment[] AssignmentTable = new Assignment[]
    {
      new Assignment("DEV5A1", 6, 5, "Programming"),
      new Assignment("DEV5A2", 11, 20, "Multiple-Choice"),
      new Assignment("DEV5A3", 12, 15, "Programming"),
      new Assignment("DEV6A1", 4, 5, "Multiple-Choice"),
      new Assignment("DEV6A2", 11, 15, "Multiple-Choice"),
      new Assignment("DEV2A1", 10, 5, "Programming"),
      new Assignment("DEV2A2", 6, 15, "Programming")
    };

    public static CourseAssignment[] CourseAssignmentTable = new CourseAssignment[]
    {
      new CourseAssignment("INFDEV03-5", "DEV5A1"),
      new CourseAssignment("INFDEV03-5", "DEV5A2"),
      new CourseAssignment("INFDEV03-5", "DEV5A3"),
      new CourseAssignment("INFDEV036A", "DEV6A1"),
      new CourseAssignment("INFDEV036A", "DEV6A2"),
      new CourseAssignment("INFDEV02-2", "DEV2A1"),
      new CourseAssignment("INFDEV02-2", "DEV2A2")
    };

    static void Main(string[] args)
    {
      var q1 = AssignmentTable.Map(//TODO: question 3);
      var q2 = CourseTable.Reduce(
        new List<Course>(),
        (filteredCourses, c) =>
        {
          //TODO: question 4
        }
        ).Select(c => new { Code = c.Code, Name = c.Name });
      var q3 = CourseTable.Join(
        CourseAssignmentTable,
        (c, ca) => c.Code == ca.CourseCode)
        .Join(
          AssignmentTable,
          /*TODO: question 4*/).Reduce(
          new List<Tuple<Tuple<Course, CourseAssignment>, Assignment>>(),
          (l, x) =>
          {
            if (x.Item2.Month < 12)
              l.Add(x);
            return l;
          }
        )
        .Select(x => new { Code = x.Item2.Code, Month = x.Item2.Month, Day = x.Item2.Day });
    }
  }
}
