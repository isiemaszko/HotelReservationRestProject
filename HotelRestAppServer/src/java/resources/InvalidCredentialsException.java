package resources;

import javax.xml.ws.WebFault;

@WebFault
public class InvalidCredentialsException extends Exception {

    public InvalidCredentialsException() {
        super();
    }
}
