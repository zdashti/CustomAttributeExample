﻿// See https://aka.ms/new-console-template for more information

using CustomAttributeExample;
using Newtonsoft.Json;
using System.Dynamic;
using System.Text;
using System.Text.Json.Nodes;

Console.WriteLine("Hello, World!");


var people = Enumerable.Repeat(new Person() { Id = 1, Name = "Ali Alavi", Phone = "09128903456", Role = "Admin", UserName = "a.alavi" }, 10000000).ToList();

//    new List<Person>()
//{
//    new (){Id = 1, Name = "Ali Alavi",Phone = "09128903456",Role = "Admin",UserName = "a.alavi"},
//    new (){Id = 2, Name = "Mina Sabori",Phone = "09128273456",Role = "Visitor",UserName = "m.sabori"},
//    new (){Id = 3, Name = "Reza Salehi",Phone = "09120983456",Role = "Developer",UserName = "r.salehi"},
//    new (){Id = 4, Name = "Sahar Kabiri",Phone = "09134403456",Role = "Visitor",UserName = "s.kabiri"},
//};


var text = new StringBuilder();

var watch = new System.Diagnostics.Stopwatch();
watch.Start();
FirstWay(people, text);
watch.Stop();
Console.WriteLine("FirstWay : " + watch.ElapsedMilliseconds);

var poco = text.ToString();
text = new StringBuilder();

watch = new System.Diagnostics.Stopwatch();
watch.Start();
FirstWay2(people, text);
watch.Stop();
Console.WriteLine("FirstWay2 : " + watch.ElapsedMilliseconds);

poco = text.ToString();
text = new StringBuilder();

watch = new System.Diagnostics.Stopwatch();
watch.Start();
FirstWay3(people, text);
watch.Stop();
Console.WriteLine("FirstWay3 : " + watch.ElapsedMilliseconds);

poco = text.ToString();
text = new StringBuilder();

watch = new System.Diagnostics.Stopwatch();
watch.Start();
FirstWay4(people, text);
watch.Stop();
Console.WriteLine("FirstWay4 : " + watch.ElapsedMilliseconds);

poco = text.ToString();
text = new StringBuilder();

watch = new System.Diagnostics.Stopwatch();
watch.Start();
FirstWay5(people, text);
watch.Stop();
Console.WriteLine("FirstWay5 : " + watch.ElapsedMilliseconds);

poco = text.ToString();
text = new StringBuilder();

watch = new System.Diagnostics.Stopwatch();
watch.Start();
FirstWay6(people, text);
watch.Stop();
Console.WriteLine("FirstWay6 : " + watch.ElapsedMilliseconds);

poco = text.ToString();
text = new StringBuilder();

//watch = new System.Diagnostics.Stopwatch();
//watch.Start();
//SecondWay(people, text);
//watch.Stop();
//Console.WriteLine("SecondWay : " + watch.ElapsedMilliseconds);

//poco = text.ToString();
//text = new StringBuilder();

void SecondWay(List<Person> persons, StringBuilder stringBuilder)
{

    foreach (var person in persons)
    {
        var props = person.GetType().GetProperties().Where(
            prop => Attribute.IsDefined(prop, typeof(LogAttribute)));

        foreach (var property in props)
        {
            var rr = property.PropertyType;
            if (rr == typeof(int))
            {
                Func<Person, int> getter = (Func<Person, int>)Delegate.CreateDelegate(typeof(Func<Person, int>), null, typeof(Person).GetProperty(property.Name).GetGetMethod());
                stringBuilder.Append($"Name : {property.Name} - Value :{getter(person)}");
            }
            else if (rr == typeof(string))
            {
                Func<Person, string> getter = (Func<Person, string>)Delegate.CreateDelegate(typeof(Func<Person, string>), null, typeof(Person).GetProperty(property.Name).GetGetMethod());
                stringBuilder.Append($"Name : {property.Name} - Value :{getter(person)}");
            }
        }
    }
}

void FirstWay6(List<Person> persons, StringBuilder stringBuilder)
{
    foreach (var person in persons)
    {
        var props = person.GetType().GetProperties().Where(
            prop => Attribute.IsDefined(prop, typeof(LogAttribute)));

        var json = new JsonObject();
        foreach (var property in props)
        {
            json.Add(property.Name, property.GetValue(person) as JsonNode);
        }
        stringBuilder.Append(JsonConvert.SerializeObject(json));
    }
}

void FirstWay5(List<Person> persons, StringBuilder stringBuilder)
{
    foreach (var person in persons)
    {
        var props = person.GetType().GetProperties().Where(
            prop => Attribute.IsDefined(prop, typeof(LogAttribute)));

        dynamic sampleObject = new ExpandoObject();
        foreach (var property in props)
        {
            ((IDictionary<string, object>)sampleObject).Add(property.Name, property.GetValue(person));
        }
        stringBuilder.Append(JsonConvert.SerializeObject(sampleObject));
    }
}

void FirstWay4(List<Person> persons, StringBuilder stringBuilder)
{
    foreach (var person in persons)
    {
        var props = person.GetType().GetProperties().Where(
            prop => Attribute.IsDefined(prop, typeof(LogAttribute)));

        var dict = new Dictionary<string, object?>();
        foreach (var property in props)
        {
            dict[property.Name] = property.GetValue(person);
        }

        var eo = new ExpandoObject();
        var eoColl = (ICollection<KeyValuePair<string, object>>)eo;

        foreach (var kvp in dict)
        {
            eoColl.Add(kvp);
        }

        stringBuilder.Append(JsonConvert.SerializeObject(eo));
    }
}

void FirstWay3(List<Person> persons, StringBuilder stringBuilder)
{
    foreach (var person in persons)
    {
        var props = person.GetType().GetProperties().Where(
            prop => Attribute.IsDefined(prop, typeof(LogAttribute)));

        var dict = new Dictionary<string, object?>();
        foreach (var property in props)
        {
            dict[property.Name] = property.GetValue(person);
        }

        dynamic eo = dict.Aggregate(new ExpandoObject() as IDictionary<string, object>,
            (a, p) => { a.Add(p.Key, p.Value); return a; });
        stringBuilder.Append(JsonConvert.SerializeObject(eo));
    }
}

void FirstWay2(List<Person> persons, StringBuilder stringBuilder)
{
    foreach (var person in persons)
    {
        var props = person.GetType().GetProperties().Where(
            prop => Attribute.IsDefined(prop, typeof(LogAttribute)));

        var dict = new Dictionary<string, object?>();
        foreach (var property in props)
        {
            dict[property.Name] = property.GetValue(person);
        }
        stringBuilder.Append(JsonConvert.SerializeObject(dict));
    }
}

void FirstWay(List<Person> persons, StringBuilder stringBuilder)
{
    foreach (var person in persons)
    {
        var props = person.GetType().GetProperties().Where(
            prop => Attribute.IsDefined(prop, typeof(LogAttribute)));
        foreach (var property in props)
        {

            stringBuilder.Append($"Name : {property.Name} - Value :{property.GetValue(person)}");
        }
    }
}
//Console.WriteLine(text.ToString());
//foreach (var person in people)
//{
//    var props = person.GetType().GetProperties().Where(
//        prop => Attribute.IsDefined(prop, typeof(LogAttribute)));

//    foreach (var property in props)
//    {
//        var myAttribute = property.GetCustomAttributes(true).FirstOrDefault(x => Attribute.IsDefined(property, typeof(LogAttribute))) as LogAttribute;
//        //var MyAttribute = property.CustomAttributes.FirstOrDefault(x => Attribute.IsDefined(property, typeof(LogAttribute))) as LogAttribute;

//        Console.WriteLine($"Name : {property.Name} - Value :{property.GetValue(person)} - Attribute : {myAttribute.LogLevel}");
//    }
//}