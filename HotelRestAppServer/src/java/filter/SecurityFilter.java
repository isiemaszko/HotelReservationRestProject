/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package filter;

import java.io.IOException;
import java.net.URI;
import java.util.Arrays;
import java.util.Base64;
import java.util.Collection;
import java.util.List;
import java.util.NoSuchElementException;
import java.util.StringTokenizer;
import javax.ws.rs.container.ContainerRequestContext;
import javax.ws.rs.container.ContainerRequestFilter;
import javax.ws.rs.core.MultivaluedMap;
import javax.ws.rs.core.Request;
import javax.ws.rs.core.Response;
import javax.ws.rs.ext.Provider;
import javax.xml.ws.handler.MessageContext;
import model.User;
import model.Users;

/**
 *
 * @author izabe
 */
@Provider
public class SecurityFilter implements ContainerRequestFilter{

    List<String> testedMethodNames = Arrays.asList(
            "getReservations",
            "makeReservation",
            "modifyReservation",
            "cancelReservation",
            "requestReservationConfirmation");
    
    @Override
    public void filter(ContainerRequestContext requestContext) throws IOException {
        
       List<String> isRequest=requestContext.getHeaders().get("authorization");
       
        String path= requestContext.getUriInfo().getPath();
        String[] methodsname= path.split("/",3);
        if(!testedMethodNames.contains(methodsname[1])) {return; }
        
       if(isRequest!=null && isRequest.size()>0){
           String authToken=isRequest.get(0);
           String authToken2=authToken.replaceFirst("Basic ", "");
           
           byte[] bytes=authToken2.getBytes();
           byte[] decodeBytes2=Base64.getDecoder().decode(bytes);
           String decodeStr2=new String(decodeBytes2);
           
           StringTokenizer tokenizer=new StringTokenizer(decodeStr2,":");
           String username=tokenizer.nextToken();
           String password=tokenizer.nextToken();
           
          
             Response unauthorizeStatus=Response
               .status(Response.Status.UNAUTHORIZED)
               .entity("Brak dostępu")
               .build();    
                try {
                    User user = Users.findUser(username);
                    if (!user.isPasswordValid(password.toCharArray()))
                        requestContext.abortWith(unauthorizeStatus);
                } catch (NoSuchElementException e) {
                    requestContext.abortWith(unauthorizeStatus);
                }
                return;
           
       }
     
       Response unauthorizeStatus=Response
               .status(Response.Status.UNAUTHORIZED)
               .entity("Brak dostępu")
               .build();  
       requestContext.abortWith(unauthorizeStatus);
    }
    
}
