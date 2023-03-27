using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
class ItemForm
{
    public string A;
    public string B;

    ItemForm(string _A, string _B)
    {
        A = _A;
        B = _B;
    }
}

[System.Serializable]
class DataForm
{
    public string Name;
    public string Age;

    ItemForm itemForm;

    DataForm(string _name, string _age)
    {
        Name = _name;
        Age = _age;
    }
}

public class DataManager : MonoBehaviour
{
    void Start()
    {
        var JsonData = Resources.Load<TextAsset>("saveFile/Data");

        print(JsonData.ToString());

        DataForm form = JsonUtility.FromJson<DataForm>(JsonData.ToString());

        print(form.Name);
        print(form.Age);

        form.Name = "¿”≤©¡§";
        form.Age = "38";

        string data = JsonUtility.ToJson(form);

        print(data);
    }
}