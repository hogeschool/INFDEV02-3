package chapter1;

public class Movie {
    String title;
    String genre;
    int rating;


    void playIt(){
        System.out.printf("Playing the movie: %s", title);
        setRating(10);
        rating = 10;
    }

    public void setRating(int rating){
        if(rating >= 0 && rating <= 10) {
            this.rating = rating;
        }else{
            System.err.println("foute rating gegeven");
        }
    }


}
