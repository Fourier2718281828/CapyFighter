using System;

public class PossibleAssignment : IComparable<PossibleAssignment>
{
    private int _score;
    private AIObject _possibleTaskDoer;

    public Task TaskToDo { get; private set; }

    public PossibleAssignment(Task taskToDo, AIObject taskDoer)
    {
        if (taskToDo == null)
            throw new ArgumentNullException("Cannot instantiate assignment with a null task.");
        if (taskDoer == null)
            throw new ArgumentNullException("Cannot instantiate assignment with a null task doer.");

        TaskToDo = taskToDo;
        _possibleTaskDoer = taskDoer;
        _score = EvaluateScore();
    }

    public void Assign()
    {
        if (TaskToDo.IsAssigned()) return;
        TaskToDo.Assign(_possibleTaskDoer);
        _possibleTaskDoer?.Assign(this);
    }

    private int EvaluateScore()
    {
        return TaskToDo.Priority;
    }

    public int CompareTo(PossibleAssignment other)
    {
        return _score.CompareTo(other._score);
    }
}