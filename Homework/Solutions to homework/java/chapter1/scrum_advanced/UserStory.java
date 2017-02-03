package chapter1.scrum_advanced;

public class UserStory {
    private String description;
    private int hours;
    private Progress progress;

    public UserStory(String description, int hours){
        this.description = description;
        this.hours = hours;
        this.progress = Progress.TODO;
    }

    public UserStory(String description, int hours, Progress progress) {
        this.description = description;
        this.hours = hours;
        this.progress = progress;
    }

    public int getHours() {
        return hours;
    }

    /**
     * Sets the estimated hours for this userstory.
     * It is priivate because once the hours are set (through the constructor),
     * changing the hours isn't allowed and possible.
     * @param hours
     */
    private void setHours(int hours) {
        this.hours = hours;
    }

    public String getDescription() {
        return description;
    }

    /**
     * Sets the description for this userstory (the actual story)
     * @param description
     */
    private void setDescription(String description) {
        this.description = description;
    }

    public Progress getProgress() {
        return progress;
    }

    public void setProgress(Progress progress) {
        this.progress = progress;
    }

    @Override
    public String toString() {
        return "UserStory{" +
                "description='" + description + '\'' +
                ", hours=" + hours +
                ", progress=" + progress +
                '}';
    }

    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;

        UserStory userStory = (UserStory) o;

        if (hours != userStory.hours) return false;
        if (description != null ? !description.equals(userStory.description) : userStory.description != null)
            return false;
        return progress == userStory.progress;

    }


}
