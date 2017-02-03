package chapter1.samples;

public class UserStory {
    int hours;
    String description;

    public int getHours() {
        return hours;
    }

    public void setHours(int hours) {
        this.hours = hours;
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    //zelfde als __str__
    public String toString(){
        return getDescription() + ": " + getHours();
    }

    public static void main(String[] args) {
        UserStory us = new UserStory();
        us.setDescription("Als gebruiker wil ik kaas eten. veel!");
        us.setHours(10);

        System.out.println(us);
    }

}
