package chapter1.scrum;

public class UserStory {
    String description;
    int hours;
    String status;

    public UserStory(){
    }

    public UserStory(String description, int hours){
        this.description = description;
        this.hours = hours;
    }

    public UserStory(String description, int hours, String status) {
        this.description = description;
        this.hours = hours;
        this.status = status;
    }

    public int getHours() {
        return hours;
    }

    private void setHours(int hours) {
        this.hours = hours;
    }

    public String getDescription() {
        return description;
    }

    private void setDescription(String description) {
        this.description = description;
    }

    public String getStatus() {
        return status;
    }

    public void setStatus(String status) {
        this.status = status;
    }

    @Override
    public String toString(){
        return getDescription() + ": " + getHours();
    }

    public boolean equals(UserStory that){
        return this.getDescription().equals(that.getDescription());
    }


}
