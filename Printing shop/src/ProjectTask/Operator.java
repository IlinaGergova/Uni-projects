package ProjectTask;

public class Operator extends Employee{
    private double salary;

    public Operator(double salary){
        super(salary);
        this.salary = salary;
    }

    public double getSalary(){
        return salary;
    }
}
