package chapter4.car;

import java.time.Duration;

public interface Entity {
    void tick(Duration duration);

    Position getPositionX();
}
