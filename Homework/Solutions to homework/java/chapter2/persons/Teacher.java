package chapter2.persons;

public class Teacher implements Person {
    private String name;
    private int age;

    @Override
    public String getName() {
        return name;
    }

    @Override
    public String getSurname() {
        return null;
    }

    @Override
    public int getAge() {
        return age;
    }

    public void incrementAge(){
        age++;
    }

    public static void main(String[] args) {
        Teacher teacher1 = new Teacher();
        teacher1.incrementAge();
        teacher1.incrementAge();
        teacher1.incrementAge();
        teacher1.incrementAge();
        Teacher teacher2 = new Teacher();
        Teacher teacher3 = new Teacher();
        Teacher teacher4 = new Teacher();

        teacher1.age = 10;
        System.out.println(teacher1.getName() + " = " + teacher1.getAge() );
    }
}
