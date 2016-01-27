package exercises.exercise4;

import java.time.Duration;

public interface Entity {
    void tick(Duration duration);

    Position getPositionX();
}
