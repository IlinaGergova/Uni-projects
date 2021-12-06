package ProjectTask;

public class Manager extends Employee{
    private double percentage;
    private double salary;

    public Manager(double salary, double percentage){
        super(salary);
        this.salary = salary;
        this.percentage = percentage;
    }

    public double increaseSalary(){
        double add = salary + percentage/100*salary;
        return salary + add;
    }

    public double getSalary(){
        return salary;
    }
}
