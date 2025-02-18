using Microsoft.Data.SqlClient;
using System.ComponentModel.Design;
using System.Runtime.Intrinsics.X86;
class Program
{
    public static void Main(string[] args)
    {
        //CreateDatabase();
        //Tables();
        //Inserts();
        Selects();

    }
    public static void CreateDatabase()
    {
        string connectionString = "Data Source=(localdb)\\ProjectModels;Integrated Security=True;" +
            "Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;MultipleActiveResultSets=True";
        string createDbQuery = "IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'school') CREATE DATABASE school;";
        try
        {
            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(createDbQuery, sqlConnection))
                {
                    sqlCommand.ExecuteNonQuery();
                    Console.WriteLine("Database 'school' created successfully or already exists.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }

    }
    public static void Tables()
    {
        string connectionString = "Data Source=(localdb)\\ProjectModels;Initial Catalog=school;Integrated Security=True;" +
            "Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;MultipleActiveResultSets=True";

        string table1 = @"
USE school;
        IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='subjects' AND xtype='U')
        CREATE TABLE subjects
        (
        [subjects_id] INT PRIMARY KEY ,
        [titile] NVARCHAR(100) NOT NULL,
        [level] INT NOT NULL
        );";

        string table2 = @"
USE school;
        IF NOT EXISTS(SELECT * FROM sysobjects WHERE name = 'teachers' AND xtype = 'U')
        CREATE TABLE teachers
        (
        [teacher_id] INT PRIMARY KEY ,
        [teacher_code] NVARCHAR(50) NOT NULL,
        [full_name] NVARCHAR(100) NOT NULL,
        [gender] CHAR(1) CHECK([GENDER] = 'F' OR [gender] = 'M') NOT NULL,
        [date_of_birth] DATE NOT NULL,
        [email] NVARCHAR(320) UNIQUE NOT NULL,
        [phone] NVARCHAR(100) UNIQUE NOT NULL,
        [working_days] INT NOT NULL
        );";

        string table3 = @"
USE school;
            IF NOT EXISTS(SELECT * FROM sysobjects WHERE name = 'classrooms' AND xtype = 'U')
        CREATE TABLE classrooms
        (
        [classroom_id] INT PRIMARY KEY ,
        [capacity] INT NOT NULL CHECK(capacity > 0),
        [description] NVARCHAR(255)
        );";

        string table4 = @"
USE school;
            IF NOT EXISTS(SELECT * FROM sysobjects WHERE name = 'classes' AND xtype = 'U')
        CREATE TABLE classes
        (
        [classes_id] INT PRIMARY KEY ,
        [class_number] INT NOT NULL,
        [class_letter] NVARCHAR(100) NOT NULL,
        [class_teacher_id] INT,
        [classroom_id] INT,
        FOREIGN KEY([class_teacher_id]) REFERENCES teachers([teacher_id]),
        FOREIGN KEY([classroom_id]) REFERENCES classrooms([classroom_id])
        );";

        string table5 = @"
USE school;
        IF NOT EXISTS(SELECT * FROM sysobjects WHERE name = 'students' AND xtype = 'U')
        CREATE TABLE students
        (
        [students_id] INT PRIMARY KEY ,
        [student_code] NVARCHAR(50) NOT NULL UNIQUE,
        [full_name] NVARCHAR(100) NOT NULL,
        [gender] CHAR(1) CHECK([GENDER] = 'F' OR[gender] = 'm') NOT NULL,
        [date_of_birth] DATE NOT NULL,
        [email] NVARCHAR(320) UNIQUE not null,
        [phone] NVARCHAR(100) UNIQUE not null,
        [class_id] INT,
        FOREIGN KEY([class_id]) REFERENCES classes([classes_id]),
        [is_active] NVARCHAR(100)
        );
        ALTER TABLE students
        ALTER COLUMN [date_of_birth] DATE NOT NULL;";

        string table6 = @"
USE school;
            IF NOT EXISTS(SELECT * FROM sysobjects WHERE name = 'parents' AND xtype = 'U')
        CREATE TABLE parents
        (
        [parents_id] INT PRIMARY KEY ,
        [parent_code] INT NOT NULL,
        [full_name] NVARCHAR(100) NOT NULL,
        [email] NVARCHAR(320) UNIQUE NOT NULL,
        [phone] NVARCHAR(100) UNIQUE NOT NULL
        );";

        string table7 = @"
USE school;
            IF NOT EXISTS(SELECT * FROM sysobjects WHERE name = 'teacher_subjects' AND xtype = 'U')
        CREATE TABLE teacher_subjects
        (
        [teacher_subjects_id] INT PRIMARY KEY ,
        [teacher_id] INT,
        [subject_id] INT,
        FOREIGN KEY([teacher_id]) REFERENCES teachers([teacher_id]),
        FOREIGN KEY([subject_id]) REFERENCES subjects([subjects_id])
        );";

        string table8 = @"
USE school;
            IF NOT EXISTS(SELECT * FROM sysobjects WHERE name = 'classes_subjects' AND xtype = 'U')
        CREATE TABLE classes_subjects
        (
        [classes_subjects_id] INT PRIMARY KEY ,
        [classes_id] INT,
        [subject_id] INT,
        FOREIGN KEY([classes_id]) REFERENCES classes([classes_id]),
        FOREIGN KEY([subject_id]) REFERENCES subjects([subjects_id])
        );";

        string table9 = @"
USE school;
            IF NOT EXISTS(SELECT * FROM sysobjects WHERE name = 'students_parents' AND xtype = 'U')
        CREATE TABLE students_parents
        (
        [students_parents_id] INT PRIMARY KEY ,
        [student_id] INT,
        [parent_id] INT
        FOREIGN KEY([student_id]) REFERENCES students([students_id]),
        FOREIGN KEY([parent_id]) REFERENCES parents([parents_id])
        );";

        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            sqlConnection.Open();
            using (SqlCommand sqlCommand = new SqlCommand(table1, sqlConnection))
            {
                sqlCommand.ExecuteNonQuery();
                Console.WriteLine("Table 1 is created or already exist.");
            }
            using (SqlCommand sqlCommand = new SqlCommand(table2, sqlConnection))
            {
                sqlCommand.ExecuteNonQuery();
                Console.WriteLine("Table 2 is created or already exist.");
            }
            using (SqlCommand sqlCommand = new SqlCommand(table3, sqlConnection))
            {
                sqlCommand.ExecuteNonQuery();
                Console.WriteLine("Table 3 is created or already exist.");
            }
            using (SqlCommand sqlCommand = new SqlCommand(table4, sqlConnection))
            {
                sqlCommand.ExecuteNonQuery();
                Console.WriteLine("Table 4 is created or already exist.");
            }
            using (SqlCommand sqlCommand = new SqlCommand(table5, sqlConnection))
            {
                sqlCommand.ExecuteNonQuery();
                Console.WriteLine("Table 5 is created or already exist.");
            }
            using (SqlCommand sqlCommand = new SqlCommand(table6, sqlConnection))
            {
                sqlCommand.ExecuteNonQuery();
                Console.WriteLine("Table 6 is created or already exist.");
            }
            using (SqlCommand sqlCommand = new SqlCommand(table7, sqlConnection))
            {
                sqlCommand.ExecuteNonQuery();
                Console.WriteLine("Table 7 is created or already exist.");
            }
            using (SqlCommand sqlCommand = new SqlCommand(table8, sqlConnection))
            {
                sqlCommand.ExecuteNonQuery();
                Console.WriteLine("Table 8 is created or already exist.");
            }
            using (SqlCommand sqlCommand = new SqlCommand(table9, sqlConnection))
            {
                sqlCommand.ExecuteNonQuery();
                Console.WriteLine("Table 9 is created or already exist.");
            }
        }

    }
    public static void Inserts()
    {

        string connectionString = "Data Source=(localdb)\\ProjectModels;Initial Catalog=school;Integrated Security=True;" +
            "Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;MultipleActiveResultSets=True";

        string insert1 = @"
         -- Insert into subjects
        INSERT INTO subjects (titile, level, subjects_id) 
        VALUES 
        ('English', 1, 1), 
        ('Biology', 2, 2), 
        ('Geography', 1, 3);";

        string insert2 = @"
         -- Insert teachers if they do not already exist
        -- Insert into teachers
        INSERT INTO teachers (teacher_code, full_name, gender, date_of_birth, email, phone, working_days, teacher_id) 
        VALUES 
        (2001, 'David Brown', 'M', '1982-08-14', 'david.brown@example.com', '2223334444', 5, 1),
        (2002, 'Emily Clark', 'F', '1979-11-30', 'emily.clark@example.com', '5556667777', 4, 2);";


        string insert3 = @"
        -- Insert into classrooms
        INSERT INTO classrooms (capacity, description, classroom_id)
        VALUES 
        (35, 'Main Building Room A', 1), 
        (20, 'Annex Room B', 2);";


        string insert4 = @"
        -- Insert into classes
        INSERT INTO classes (classes_id, class_number, class_letter, class_teacher_id, classroom_id) 
        VALUES 
        (1, 1, 'A', 1, 1), 
        (2, 2, 'B', 2, 2);";

        string insert5 = @"
        -- Insert into students
        INSERT INTO students (students_id, student_code, full_name, gender, date_of_birth, email, phone, class_id, is_active) 
        VALUES 
        (1, 'S003', 'Charlie Adams', 'M', '2010-06-18', 'charlie.a@example.com', '99988877777', 1, 'Active'),
        (2, 'S004', 'Diana Evans', 'F', '2011-10-05', 'diana.e@example.com', '66677788888', 2, 'Active');";


        string insert6 = @"
        -- Insert into parents
        INSERT INTO parents (parents_id, parent_code, full_name, email, phone) 
        VALUES 
        (1, 3001, 'Robert Adams', 'robert.a@example.com', '11100009999'),
        (2, 3002, 'Linda Evans', 'linda.e@example.com', '33322221111');";


        string insert7 = @"
        -- Insert into teacher_subjects
        INSERT INTO teacher_subjects (teacher_subjects_id, teacher_id, subject_id) 
        VALUES 
        (1, 1, 1),  -- David Brown teaches English
        (2, 2, 2);  -- Emily Clark teaches Biology";

        string insert8 = @"
        -- Insert into classes_subjects
        INSERT INTO classes_subjects (classes_subjects_id, classes_id, subject_id) 
        VALUES 
        (1, 1, 1),  -- Class 1A has English
        (2, 2, 2);  -- Class 2B has Biology";


        string insert9 = @"
        -- Insert into students_parents
        INSERT INTO students_parents (students_parents_id, student_id, parent_id) 
        VALUES 
        (1, 1, 1),  -- Charlie Adams' parent is Robert Adams
        (2, 2, 2);  -- Diana Evans' parent is Linda Evans";


        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            sqlConnection.Open();
            using (SqlTransaction transaction = sqlConnection.BeginTransaction())
            {
                try
                {
                    using (SqlCommand sqlCommand = new SqlCommand(insert1, sqlConnection, transaction))
                        sqlCommand.ExecuteNonQuery();
                    using (SqlCommand sqlCommand = new SqlCommand(insert2, sqlConnection, transaction))
                        sqlCommand.ExecuteNonQuery();
                    using (SqlCommand sqlCommand = new SqlCommand(insert3, sqlConnection, transaction))
                        sqlCommand.ExecuteNonQuery();
                    using (SqlCommand sqlCommand = new SqlCommand(insert4, sqlConnection, transaction))
                        sqlCommand.ExecuteNonQuery();
                    using (SqlCommand sqlCommand = new SqlCommand(insert5, sqlConnection, transaction))
                        sqlCommand.ExecuteNonQuery();
                    using (SqlCommand sqlCommand = new SqlCommand(insert6, sqlConnection, transaction))
                        sqlCommand.ExecuteNonQuery();
                    using (SqlCommand sqlCommand = new SqlCommand(insert7, sqlConnection, transaction))
                        sqlCommand.ExecuteNonQuery();
                    using (SqlCommand sqlCommand = new SqlCommand(insert8, sqlConnection, transaction))
                        sqlCommand.ExecuteNonQuery();
                    using (SqlCommand sqlCommand = new SqlCommand(insert9, sqlConnection, transaction))
                        sqlCommand.ExecuteNonQuery();

                    transaction.Commit();
                    Console.WriteLine("Data inserted successfully!");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();  // Rollback transaction if an error occurs
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
    public static void Selects()
    {
        string connectionString = "Data Source=(localdb)\\ProjectModels;Initial Catalog=school;Integrated Security=True;" +
           "Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;MultipleActiveResultSets=True";

        string select1 = "SELECT * FROM subjects;";

        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            sqlConnection.Open();

            using (SqlCommand sqlCommand = new SqlCommand(select1, sqlConnection))
            {
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    Console.WriteLine("1 Query:");
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["subjects_id"]}: {reader["titile"]}, Level {reader["level"]}");
                    }
                }
            }
        }
        Console.WriteLine();

        string select2 = @"
            SELECT subjects.titile, teachers.full_name
            FROM teacher_subjects
            JOIN teachers ON teacher_subjects.teacher_id = teachers.teacher_id
            JOIN subjects ON teacher_subjects.subject_id = subjects.subjects_id
            ORDER BY subjects.titile, teachers.full_name;";



        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            sqlConnection.Open();

            using (SqlCommand sqlCommand = new SqlCommand(select2, sqlConnection))
            {
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    Console.WriteLine("2 Query:");
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["titile"]}, {reader["full_name"]}");
                    }
                }
            }
        }
        Console.WriteLine();


        string select3 = @"
            SELECT classes.class_number, classes.class_letter, teachers.full_name AS class_teacher
            FROM classes
            JOIN teachers ON classes.class_teacher_id = teachers.teacher_id;";

        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            sqlConnection.Open();

            using (SqlCommand sqlCommand = new SqlCommand(select3, sqlConnection))
            {
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    Console.WriteLine("3 Query:");
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["class_number"]},{reader["class_letter"]},{reader["class_teacher"]}");
                    }
                }
            }
        }
        Console.WriteLine();


        string select4 = @"
            SELECT subjects.titile, COUNT(teacher_subjects.teacher_id) AS teacher_count
            FROM subjects
            LEFT JOIN teacher_subjects ON subjects.subjects_id = teacher_subjects.subject_id
            GROUP BY subjects.titile;
";

        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            sqlConnection.Open();

            using (SqlCommand sqlCommand = new SqlCommand(select4, sqlConnection))
            {
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    Console.WriteLine("4 Query:");
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["titile"]}, {reader["teacher_count"]}");
                    }
                }
            }
        }
        Console.WriteLine();


        string select5 = @"
            SELECT classroom_id, capacity
            FROM classrooms
            WHERE capacity > 26
            ORDER BY capacity ASC;
";

        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            sqlConnection.Open();

            using (SqlCommand sqlCommand = new SqlCommand(select5, sqlConnection))
            {
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    Console.WriteLine("5 Query:");
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["classroom_id"]}, {reader["capacity"]}");
                    }
                }
            }
        }
        Console.WriteLine();


        string select6 = @"
            SELECT classes.class_number, classes.class_letter, students.full_name
            FROM students
            JOIN classes ON students.class_id = classes.classes_id
            ORDER BY classes.class_number, classes.class_letter, students.full_name;
";

        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            sqlConnection.Open();

            using (SqlCommand sqlCommand = new SqlCommand(select6, sqlConnection))
            {
                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    Console.WriteLine("6 Query:");
                    while (reader.Read())
                    {
                        Console.WriteLine($"{reader["class_number"]}, {reader["class_letter"]}, {reader["full_name"]}");
                    }
                }
            }
        }

        /* string select7 = @"
             DECLARE @class_number INT, @class_letter NVARCHAR(1);
             SET @class_number = '?';  -- Въведете числото от конзолата
             SET @class_letter = '?';  -- Въведете буквата от конзолата
             SELECT students.full_name 
             FROM students
             JOIN classes ON students.class_id = classes.classes_id
             WHERE classes.class_number = @class_number AND classes.class_letter = @class_letter;
 ";

         using (SqlConnection sqlConnection = new SqlConnection(connectionString))
         {
             sqlConnection.Open();

             using (SqlCommand sqlCommand = new SqlCommand(select7, sqlConnection))
             {
                 Console.Write("Insert a class number: ");
                 int classNumber = int.Parse(Console.ReadLine());

                 Console.Write("Inert a class letter: ");
                 string classLetter = Console.ReadLine();

                 sqlCommand.Parameters.AddWithValue("@input_class_number", classNumber);
                 sqlCommand.Parameters.AddWithValue("@input_class_letter", classLetter);

                 using (SqlDataReader reader = sqlCommand.ExecuteReader())
                 {
                     Console.WriteLine("\nУченици в този клас:");

                     if (!reader.HasRows)
                     {
                         Console.WriteLine("Няма намерени ученици.");
                     }
                     else
                     {
                         while (reader.Read())
                         {
                             Console.WriteLine(reader["full_name"]);
                         }
                     }
                 }
             }
         }*/
        string select7 = @"
            SELECT students.full_name 
            FROM students
            JOIN classes ON students.class_id = classes.classes_id
            WHERE classes.class_number = @class_number 
            AND classes.class_letter = @class_letter;
";

        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            sqlConnection.Open();

            using (SqlCommand sqlCommand = new SqlCommand(select7, sqlConnection))
            {
                Console.WriteLine("7 Query: ");
                Console.Write("Insert a class number: ");
                int classNumber;
                while (!int.TryParse(Console.ReadLine(), out classNumber))
                {
                    Console.WriteLine("Please add a valid number!");
                    Console.Write("Insert a class number: ");
                }

                Console.Write("Insert a class letter: ");
                string classLetter = Console.ReadLine();

                sqlCommand.Parameters.AddWithValue("@class_number", classNumber);
                sqlCommand.Parameters.AddWithValue("@class_letter", classLetter);

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    Console.WriteLine("\nStudents in this class:");

                    if (!reader.HasRows)
                    {
                        Console.WriteLine("No students found.");
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine(reader["full_name"]);
                        }
                    }
                }
            }
        }
        Console.WriteLine();




        string select8 = @"
            SELECT full_name 
            FROM students
            WHERE date_of_birth = @birth_date;
";

        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            sqlConnection.Open();

            using (SqlCommand sqlCommand = new SqlCommand(select8, sqlConnection))
            {
                Console.WriteLine("8 Query: ");
                Console.Write("Insert the data as YYYY-MM-DD: ");
                DateTime birthDate;
                while (!DateTime.TryParse(Console.ReadLine(), out birthDate))
                {
                    Console.WriteLine("Invalid format! Please use the data as YYYY-MM-DD. ");  //2010-06-18
                    Console.Write("INsert date of birth");
                }

                sqlCommand.Parameters.AddWithValue("@birth_date", birthDate);

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    Console.WriteLine("\nStudents, born on this date:");
                    if (!reader.HasRows)
                    {
                        Console.WriteLine("No students found.");
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine(reader["full_name"]);
                        }
                    }
                }
            }
        }
        Console.WriteLine();



        string select9 = @"
            SELECT COUNT(DISTINCT cs.subject_id) AS subject_count
            FROM students s
            JOIN classes c ON s.class_id = c.classes_id
            JOIN classes_subjects cs ON c.classes_id = cs.classes_id
            WHERE s.full_name = @student_name;
";

        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            sqlConnection.Open();

            using (SqlCommand sqlCommand = new SqlCommand(select9, sqlConnection))
            {
                Console.WriteLine("9 Query: ");
                Console.Write("Insert a student name: ");  //Charlie Adams, Diana Evans
                string studentName = Console.ReadLine();

                sqlCommand.Parameters.AddWithValue("@student_name", studentName);

                object result = sqlCommand.ExecuteScalar();
                int subjectCount = result != DBNull.Value ? Convert.ToInt32(result) : 0;

                Console.WriteLine($"\nThe student {studentName} learns {subjectCount} subjects.");
            }
        }
        Console.WriteLine();





        string select10 = @"
            SELECT t.full_name AS teacher_name, s.titile AS subject_name
            FROM students st
            JOIN classes c ON st.class_id = c.classes_id
            JOIN classes_subjects cs ON c.classes_id = cs.classes_id
            JOIN subjects s ON cs.subject_id = s.subjects_id
            JOIN teacher_subjects ts ON s.subjects_id = ts.subject_id
            JOIN teachers t ON ts.teacher_id = t.teacher_id
            WHERE st.full_name = @student_name;
";

        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            sqlConnection.Open();

            using (SqlCommand sqlCommand = new SqlCommand(select10, sqlConnection))
            {
                Console.WriteLine("10 Query: ");
                Console.Write("Insert a student name: "); //Charlie Adams, Diana Evans
                string studentName = Console.ReadLine();

                sqlCommand.Parameters.AddWithValue("@student_name", studentName);

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    Console.WriteLine($"\nTeachers and subjects on a students: {studentName}");
                    if (!reader.HasRows)
                    {
                        Console.WriteLine("No data found.");
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader["teacher_name"]} - {reader["subject_name"]}");
                        }
                    }
                }
            }
        }
        Console.WriteLine();


        string select11 = @"
            SELECT DISTINCT c.class_number, c.class_letter
            FROM parents p
            JOIN students_parents sp ON p.parents_id = sp.parent_id
            JOIN students s ON sp.student_id = s.students_id
            JOIN classes c ON s.class_id = c.classes_id
            WHERE p.email = @parent_email;
";

        using (SqlConnection sqlConnection = new SqlConnection(connectionString))
        {
            sqlConnection.Open();

            using (SqlCommand sqlCommand = new SqlCommand(select11, sqlConnection))
            {
                Console.WriteLine("11 Query");
                Console.Write("Insert a parent email: ");  //robert.a@example.com, linda.e@example.com
                string parentEmail = Console.ReadLine();

                sqlCommand.Parameters.AddWithValue("@parent_email", parentEmail);

                using (SqlDataReader reader = sqlCommand.ExecuteReader())
                {
                    Console.WriteLine($"\nClasses attended by the parent's children: ({parentEmail}):");
                    if (!reader.HasRows)
                    {
                        Console.WriteLine("No classes found.");
                    }
                    else
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader["class_number"]}{reader["class_letter"]}");
                        }
                    }
                }
            }
        }
    }
}