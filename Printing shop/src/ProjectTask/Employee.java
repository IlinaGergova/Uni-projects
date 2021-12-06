package ProjectTask;

public abstract class Employee {
    private double mainSalary;

    public Employee(double mainSalary){
        this.mainSalary = mainSalary;
    }

    public double getMainSalary(){
        return mainSalary;
    }

}
