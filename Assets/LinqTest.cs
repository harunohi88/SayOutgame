using NUnit.Framework;
using System.Runtime.CompilerServices;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.ShaderGraph.Legacy;

public class LinqTest : MonoBehaviour
{
    private void Start()
    {
        List<Student> students = new List<Student>()
        {
            new Student() { Name = "������", Age = 28, Gender = "��" },
            new Student() { Name = "�ڼ���", Age = 26, Gender = "��" },
            new Student() { Name = "������", Age = 29, Gender = "��" },
            new Student() { Name = "�̻���", Age = 28, Gender = "��" },
            new Student() { Name = "������", Age = 25, Gender = "��" },
            new Student() { Name = "������", Age = 27, Gender = "��" },
            new Student() { Name = "�ڼ�ȫ", Age = 27, Gender = "��" },
            new Student() { Name = "�缺��", Age = 29, Gender = "��" },
        };

        // �÷��ǿ��� �����͸� '��ȸ(����'�ϴ� ���� �����ϴ�.
        // C#�� �̷� ����� �۾��� ���ϰ� �ϱ� ���� LINQ ������
        // Language Intergrated Query
        // ����(Query) : ���� (�����͸� �������� ��ɹ�)

        // "FROM, IN, SELECT" -> �����ͺ��̽� SELECT ���� ����ϴ�
        // ��� �̷��Դ� �� ������ �ʴ´�.
        var all = from student in students
                  select student;
        // �̷��� ���δ�.
        all = students.Where(student => true);

        foreach (var item in all)
        {
            Debug.Log(item);
        }


        var mans = from student in students
                   where student.Gender == "��"
                   select student;
        mans = students.Where(student => student.Gender == "��");

        foreach(var item in mans)
        {
            Debug.Log(item);
        }

        var girls = from student in students
                    where student.Gender == "��"
                    orderby student.Age
                    select student;
        girls = students.Where(student => student.Gender == "��").OrderBy(student => student.Age);

        // ���� : ���ϰ� �������� ����.

        // ���� : 
        // IEnumerable�� ���������� Ŀ���� ����µ� �̰��� ���߿� �����Ⱑ �ȴ�.
        // �� �޸𸮰� �����Ѵ�.
        // �� ���� �� ������... ����Ƽ UPDATE���� ����� ����!

        int mansCount = students.Count(student => student.Gender == "��");
        Debug.Log($"���� �л��� {mansCount}�� �Դϴ�.");

        int totalAge = students.Sum(student => student.Age);
        Debug.Log($"�л��� �� ���̴� {totalAge} �Դϴ�.");

        // AVERAGE, 

        // ���� ����? ALL(��ΰ� �����ϴ�?) vs ANY(�ϳ� �̻��� �����ϴ�?)
        bool isAllMan = students.All(student => student.Gender == "��");

        bool is30 = students.Any(student => student.Age >= 30);

        // ���� ����
        // Sort ���� �Լ��� ���������� ����ũ�μ���Ʈ�� �̸� ������ ��Ʈ�� ��Ʈ�� ����.
        // ��Ʈ�� ��Ʈ : �������� ũ��, ���� ���� ������ ���� Quick, Heap, Radix Sort�� «���ؼ� ������ ���� ����̴�.
        students.Sort();
    }
}
