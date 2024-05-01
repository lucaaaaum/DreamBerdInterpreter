namespace DreamBerdInterpreter.Interpreter.Types;

public class VarConst
{
    public string Name { get; init; }
    private string _value;

    public string Value
    {
        get => _value;
        set => _value = CanBeAssigned
            ? value
            : throw new Exception(
                "bro you can't assign a new to value to a variable that cannot be re-assigned that's like so dumb go read the docs");
    }

    public bool CanBeEdited { get; init; }
    public bool CanBeAssigned { get; init; }
    public int VisibilityLevel { get; init; }

    public VarConst(string name, bool canBeEdited, bool canBeAssigned)
    {
        Name = name;
        CanBeEdited = canBeEdited;
        CanBeAssigned = canBeAssigned;
    }

    public VarConst(string value, string name, bool canBeEdited, bool canBeAssigned, int visibilityLevel)
    {
        _value = value;
        Name = name;
        CanBeEdited = canBeEdited;
        CanBeAssigned = canBeAssigned;
        VisibilityLevel = visibilityLevel;
    }

    public VarConst(string value, string name, bool canBeEdited, bool canBeAssigned, int visibilityLevel,
        Dictionary<string, VarConst> definitions)
    {
        _value = value;
        Name = name;
        CanBeEdited = canBeEdited;
        CanBeAssigned = canBeAssigned;
        VisibilityLevel = visibilityLevel;
        Definitions = definitions;
    }

    public Dictionary<string, VarConst> Definitions { get; init; }
}