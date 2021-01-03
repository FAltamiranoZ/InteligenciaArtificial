(defun condMov (mov libres) (equal (car (remove nil (mapcar #'(lambda (x) (when (equal mov x) x)) libres))) mov))


(defun condMovRey (mov) (equal (car (remove nil (mapcar #'(lambda (x) (when (equal mov x) x)) reservadas))) mov))


(defun cambiaposicion (id mov nodo)
(cond ( (or (condmov mov (third nodo)) (and (eq id 1) (condmovrey mov)))  
(nconc
(if (not (null (contains-list (cadr (assoc id (fourth nodo))) (car nodo)))) 
(cons (cons mov (remove nil (mapcar #'(lambda (x) (when (not (equal (cadr (assoc id (fourth nodo))) x)) x)) (car nodo)))) (list (cadr nodo)))
(cons (car nodo) (list (cons mov (remove nil (mapcar #'(lambda (x) (when (not (equal (cadr (assoc id (fourth nodo))) x)) x)) (cadr nodo)))))))
(cons
(cons (cadr (assoc id (fourth nodo))) (remove nil (mapcar #'(lambda (x) (when (not (equal mov x)) x)) (third nodo))))
(list (nconc
(reverse (cdr (member (assoc id (fourth nodo)) (reverse (fourth nodo)))))
(cons (cons id (cons mov (cddr (assoc id (fourth nodo))))) (cdr (member (assoc id (fourth nodo)) (fourth nodo))) ))) )))
(t nil)))


(defun actualizaposicion (id mov) (cond ( (or (condmov mov vacias) (and (eq id 1) (condmovrey mov)) ) (setq vacias (remove nil (mapcar #'(lambda (x) (when (not (equal mov x)) x)) vacias)))
(push (cadr (assoc id pin)) vacias)
(cond ( (or (equal (car (nthcdr 2 (assoc 1 pin))) 1) (equal (car (nthcdr 2 (assoc 1 pin))) 3) ) (setq pinn (remove nil (mapcar #'(lambda (x) (when (not (equal (cadr (assoc id pin)) x)) x)) pinn))) (push mov pinn))
(t (setq pinb (remove nil (mapcar #'(lambda (x) (when (not (equal (cadr (assoc id pin)) x)) x)) pinb))) (push mov pinb)))
(rplaca (nthcdr 1 (assoc id pin)) mov))
(t nil)))


(defun resetPos () 
(setq posibles nil)
(setq i 1)
(setq pos nil))


(defun posiblesArriba (id i nodo)
(setq pos (list (caadr (assoc id (fourth nodo))) (+ (cadadr (assoc id (fourth nodo))) i)))
(cond ((or (condmov pos (third nodo)) (and (eq id 1) (condmovrey pos))) (push pos posibles) (posiblesArriba id (+ i 1) nodo))
(t )))


(defun posiblesAbajo (id i nodo)
(setq pos (list (caadr (assoc id (fourth nodo))) (- (cadadr (assoc id (fourth nodo))) i)))
(cond ((or (condmov pos (third nodo)) (and (eq id 1) (condmovrey pos))) (push pos posibles) (posiblesAbajo id (+ i 1) nodo))
(t )))


(defun posiblesDerecha (id i nodo)
(setq pos (list (+ (caadr (assoc id (fourth nodo))) i) (cadadr (assoc id (fourth nodo)))))
(cond ((or (condmov pos (third nodo)) (and (eq id 1) (condmovrey pos))) (push pos posibles) (posiblesDerecha id (+ i 1) nodo))
(t )))


(defun posiblesIzquierda(id i nodo)
(setq pos (list (- (caadr (assoc id (fourth nodo))) i) (cadadr (assoc id (fourth nodo)))))
(cond ((or (condmov pos (third nodo)) (and (eq id 1) (condmovrey pos))) (push pos posibles) (posiblesIzquierda id (+ i 1) nodo))
(t )))


(defun posiblesF (id nodo)
(resetpos)
(posiblesArriba id i nodo)
(setq i 1)
(posiblesAbajo id i nodo)
(setq i 1)
(posiblesIzquierda id i nodo)
(setq i 1)
(posiblesDerecha id i nodo))


(defun piezaAliada (equipo coord nodo)
   (if (and (equal coord (cadr(assoc 1 pin))) (or (eq equipo 1) (eq equipo 3))) T 
      (if (member coord reservadas :test #'equal) nil 
         (if(member coord (caddr nodo) :test #'equal) T
            (cond ((or(eq equipo 1) (eq equipo 3)) (if(member coord (car nodo) :test #'equal) nil T))
                  ((eq equipo 2) (if (member coord (cadr nodo) :test #'equal) nil T)))))))


(defun piezaAliadaRey (coord nodo)
   (if(or(member coord reservadas :test 'equal) (member coord fuera :test 'equal)) nil 
      (if(member coord (caddr nodo) :test 'equal) T
         (if(member coord (car nodo) :test 'equal) nil T))))


(defun comer(equipo lst nodo)
  (cond ((null lst) nodo) ((tienecomida equipo lst nodo)
   (setq nodoaux nodo)
     (cond ((not(piezaAliada equipo(cons(+ (car lst) 1) (cdr lst)) nodoaux)) (print' entro1)
         (cond ((buscaRey(cons(+ (car lst) 1) (cdr lst)) (fourth nodoaux))
             (cond ((and (not(piezaAliadaRey(cons (+ (car lst) 2) (cdr lst)) nodoaux)) (not(piezaAliadaRey(cons (+ (car lst) 1) (list(+ (cadr lst) 1))) nodoaux)) 
                   (not(piezaAliadaRey(cons (+ (car lst) 1) (list(- (cadr lst) 1))) nodoaux)))
             (setq nodoaux (come equipo (cons(+ (car lst) 1) (cdr lst)) nodoaux)))
             (t (setq nodoaux nodoaux)))

             (t 
                 (cond ((piezaAliada equipo (cons (+ (car lst) 2) (cdr lst)) nodoaux)
                 (setq nodoaux (come equipo (cons(+ (car lst) 1) (cdr lst)) nodoaux)))
(t (setq nodoaux nodoaux)))))

   (t (setq nodoaux nodoaux)))

    (cond ((not(piezaAliada equipo(cons(car lst) (list(+ (cadr lst) 1))) nodoaux))
      (cond ((buscaRey(cons(car lst) (list(+ (cadr lst) 1))) (fourth nodoaux))
         (if (and (not(piezaAliadaRey(cons(car lst) (list(+ (cadr lst) 2))) nodoaux)) (not(piezaAliadaRey(cons (+ (car lst) 1) (list(+ (cadr lst) 1))) nodoaux)) 
                 (not(piezaAliadaRey(cons (- (car lst) 1) (list(+ (cadr lst) 1))) nodoaux)))
            (setq nodoaux (come equipo (cons(car lst) (list(+ (cadr lst) 1))) nodoaux))
 (setq nodoaux nodoaux)))

         (t (if(piezaAliada equipo (cons(car lst) (list(+ (cadr lst) 2))) nodoaux)
                     (setq nodoaux (come equipo (cons(car lst) (list(+ (cadr lst) 1))) nodoaux))
 (setq nodoaux nodoaux))) ))
   (t (setq nodoaux nodoaux)))

    (cond ((not(piezaAliada equipo(cons(- (car lst) 1) (cdr lst)) nodoaux)) 
      (cond ((buscaRey(cons(- (car lst) 1) (cdr lst)) (fourth nodoaux)) 
         (if(and (not(piezaAliadaRey(cons (- (car lst) 2) (cdr lst)) nodoaux)) (not(piezaAliadaRey(cons (- (car lst) 1) (list(+ (cadr lst) 1))) nodoaux)) 
             (not(piezaAliadaRey(cons (- (car lst) 1) (list(- (cadr lst) 1))) nodoaux)))
            (setq nodoaux (come equipo (cons(- (car lst) 1) (cdr lst)) nodoaux))
 (setq nodoaux nodoaux)))

         (t (if(piezaAliada equipo (cons (- (car lst) 2) (cdr lst)) nodoaux)
            (setq nodoaux (come equipo (cons(- (car lst) 1) (cdr lst)) nodoaux))
 (setq nodoaux nodoaux)))))
    (t (setq nodoaux nodoaux)))

    (cond ((not(piezaAliada equipo(cons(car lst) (list(- (cadr lst) 1))) nodoaux)) 
      (cond ((buscaRey(cons(car lst) (list(- (cadr lst) 1))) (fourth nodoaux))
         (if(and (not(piezaAliadaRey(cons(car lst) (list(- (cadr lst) 2))) nodoaux)) (not(piezaAliadaRey(cons (+ (car lst) 1) (list(- (cadr lst) 1))) nodoaux)) 
             (not(piezaAliadaRey(cons (- (car lst) 1) (list(- (cadr lst) 1))) nodoaux)))
            (setq nodoaux (come equipo (cons(car lst) (list(- (cadr lst) 1))) nodoaux))
 (setq nodoaux nodoaux)))

         (t (if(piezaAliada equipo (cons(car lst) (list(- (cadr lst) 2))) nodoaux)
            (setq nodoaux (come equipo (cons(car lst) (list(- (cadr lst) 1))) nodoaux))
 (setq nodoaux nodoaux)))))
   (t (setq nodoaux nodoaux)))

    nodoaux )
  (t nodo))))
 )


(defun buscaRey (coord lst)
   (equal (cadr (assoc 1 lst)) coord)
)


(defun come(equipo coord nodo)
   (if (member coord reservadas :test #'equal) nodo
   (nconc
   (cond ((eq equipo 1) (cons (remove coord (car nodo) :test #'equal) (list (cadr nodo))) ) (t (cons (car nodo) (list (remove coord (cadr nodo) :test #'equal))) ))
    (cons (cons coord (caddr nodo)) (list (remove (buscaId coord (fourth nodo)) (fourth nodo) :test #'equal)))
    )))


(defun buscaId (coord lst)
    (cond
       ((null lst) nil)
       ((equal (cadar lst) coord) (assoc (caar lst) lst))
       (t (buscaId coord (cdr lst)))
    ))


(setq alph -99999)

(setq alphaprev alph)

(setq bet 99999)

(setq betprev bet)

(setq ruta nil)

(setq mejorRuta nil)


(defun alphabeta (actual profundidad alpha beta jugador root)
         (cond
               ((esFinal actual profundidad) (valorHeuristico actual))
               
               ((eq jugador 2) (calculaMovPosibles jugador actual)(setq valorrandom (random (- (length movposibles) 1)))
                      (if (< (length movposibles) valorrandom) nil (setf movPosibles (nthcdr valorrandom movPosibles)))
                     (dolist (hijo movPosibles)
                          (cond ((null hijo))
                          (t
                          (setq alphaprev alpha)
                          (setq valorAux (alphabeta (comer jugador (cadr hijo) (cambiaPosicion (car hijo) (cadr hijo) actual)) (- profundidad 1) alpha beta 1 (nconc root (list hijo))))
                          
                          (cond ((< alpha valorAux) (setf alpha valoraux) (setf mejorRuta (nconc root (list hijo))) 
                                               (when (>= (length mejorRuta) profundidad) (setf mejorRuta (reverse (nthcdr (- (length mejorRuta) profundidad) 
                                               (reverse mejorRuta)))))) (t nil))))
                          (when (<= beta alpha)) (return))
                       
                       alpha )

              (t  (calculaMovPosibles jugador actual) (setq valorrandom (random (- (length movposibles) 1)))
                      (if (< (length movposibles) valorrandom) nil (setf movPosibles (nthcdr valorrandom movPosibles)))
                     (dolist (hijo movPosibles)
                          (cond ((null hijo))
                          (t
                          (setq betaprev beta)
                          (setq valoraux (alphabeta (comer jugador (cadr hijo) (cambiaPosicion (car hijo) (cadr hijo) actual)) (- profundidad 1) alpha beta 2 (nconc root (list hijo))))
                          
                          (cond ((> beta valorAux) (setf beta valoraux) (setf mejorRuta (nconc root (list hijo))) (when (>= (length mejorRuta) profundidad) (setf mejorRuta (reverse (nthcdr (- (length mejorRuta) profundidad) (reverse mejorRuta)))))) (t nil))))
                          (when (<= beta alpha) (return)) )
                    beta  )))


(defun esFinal (actual profundidad)
     (or
         (eq 0 profundidad)
         (not (eq 1 (car (assoc 1 (fourth actual)))))
         (equal '(1 1) (cadr (assoc 1 (fourth actual))))
         (equal '(1 11) (cadr (assoc 1 (fourth actual))))
         (equal '(11 1) (cadr (assoc 1 (fourth actual))))
         (equal '(11 11) (cadr (assoc 1 (fourth actual))))))



(defun calculaMovPosibles (equipo actual)
   (setq movPosibles nil)
   (dolist (pos (if (eq equipo 2) (car actual) (cadr actual)))
   (cond ((null pos))
   (t 
       (setq ID (car (buscaID pos (fourth actual))))
       (posiblesF ID actual)
       (dolist (movSig posibles)
         (cond ((null movSig))
           (t (push (cons ID (list movSig)) movPosibles))))))))


(setq valoresRey (make-array '(11 11) :initial-contents '((2000 60 50 40 30 20 300 40 50 60 2000)
    (60 50 40 30 20 10 20 30 40 50 60) (50 40 30 30 30 30 30 30 30 40 50) 
    (40 30 30 10 10 0 10 10 30 30 40) (30 20 30 10 0 15 0 10 30 20 30) 
    (20 10 30 0 15 0 15 0 30 10 20) (30 20 30 10 0 15 0 10 30 20 30)
    (40 30 30 10 10 0 10 10 30 30 40) (50 40 30 30 30 30 30 30 30 40 50)
    (60 50 40 30 20 10 20 30 40 50 60) (2000 60 50 40 30 20 300 40 50 60 2000))))


(setq valoresN (make-array '(11 11) :initial-contents '((nil 40 60 50 40 10 40 50 60 40 nil) 
   (40 60 50 40 30 40 30 40 50 60 40) (60 50 30 30 30 30 30 30 30 50 60)
   (50 40 30 20 10 -30 10 20 30 40 50) (40 30 30 10 -30 -50 -30 10 30 30 40)
   (10 40 30 -30 -50 nil -50 -30 30 40 10) (40 30 30 10 -30 -50 -30 10 30 30 40)
   (50 40 30 20 10 -30 10  20 30 40 50) (60 50 30 30 30 30 30 30 30 50 60)
   (40 60 50 40 30 40 30 40 50 60 40) (nil 40 60 50 40 10 40 50 60 40 nil))))


(setq valoresB (make-array '(11 11) :initial-contents '((nil -10 10 30 30 40 30 30 10 -10 nil)
   (-10 10 30 30 40 30 40 30 30 10 -10) (10 30 30 40 30 20 30 40 30 30 10)
   (30 30 40 30 20 10 20 30 40 30 30) (30 40 30 20 10 10 10 20 30 40 30)
   (40 30 20 10 10 nil 10 10 20 30 40) (30 40 30 20 10 10 10 20 30 40 30)
   (30 30 40 30 20 10 20 30 40 30 30) (10 30 30 40 30 20 30 40 30 30 10)
   (-10 10 30 30 40 30 40 30 30 10 -10) (nil -10 10 30 30 40 30 30 10 -10 nil))))


(defun buscaValor (pos lst)
       (aref lst (- (car pos) 1) (- (cadr pos) 1))
)


(defun valorpos (lst pines)
    (setq temp '())
    (dolist (pos pines)
          (if (null pos) nil
         (push (buscavalor pos lst) temp)))
    (eval (cons '+ temp)))


(defun contains-list (posicion lst)
      (cond
            ((null lst) nil)
            ((equal (car lst) posicion) (car lst))
            (t (contains-list posicion (cdr lst)))))


(defun calculaPzAlrededor (posRey libres)
      (* 50 (count-if-not #'null (list (contains-list (list (+ 1 (car posRey)) (cadr posRey)) libres) 
      (contains-list (list (- (car posRey) 1) (cadr posRey)) libres) 
      (contains-list (list (car posRey) (+ 1 (cadr posRey))) libres)
      (contains-list (list (car posRey) (- (cadr posRey) 1)) libres)))))


(defun valorHeuristico (nodo)
      (if (not (eq 1 (car (assoc 1 (fourth nodo))))) 5000
      (+ (* 10 (length (car nodo))) (calculaPzAlrededor (cadr (assoc 1 (fourth nodo))) (caddr nodo)) (- (valorpos valoresn (car nodo)) (valorpos valoresb (remove (cadr (assoc 1 pin)) (cadr nodo) :test #'equal))
                 (buscavalor (cadr (assoc 1 (fourth nodo))) valoresrey) (* 30 (length (cadr nodo)))) (* -500 (HayRey nodo))))  )


(defun HayRey (nodo)
   (count-if-not #'null (list (eq 1 (caar (fourth nodo))))))


(defun creanodo ()
   (setq nodo (cons pinn (cons pinb (cons vacias (list pin))))))


(defun tienecomida (equipo lst nodo)
  (or
   (not (piezaAliada equipo (cons(+ (car lst) 1) (cdr lst)) nodo))
   (not(piezaAliada equipo(cons(car lst) (list(+ (cadr lst) 1))) nodo))
   (not(piezaAliada equipo(cons(- (car lst) 1) (cdr lst)) nodo))
   (not(piezaAliada equipo(cons(car lst) (list(- (cadr lst) 1))) nodo))))


(setq reservadas '((1 1) (11 1) (11 11) (1 11) (6 6)))


(setq reservadasSinCentro '((1 1) (11 1) (11 11) (1 11)))


(setq fuera '((0 1) (0 2) (0 3) (0 4) (0 5) (0 6) (0 7) (0 8) (0 9) (0 10) (0 11) (12 1) (12 2) (12 3) (12 4) (12 5) (12 6) (12 7) (12 8) (12 9) (12 10) (12 11) (1 0) (2 0) (3 0) (4 0) 
(5 0) (6 0) (7 0) (8 0) (9 0) (10 0) (11 0) (1 12) (2 12) (3 12) (4 12) (5 12) (6 12) (7 12) (8 12) (9 12) (10 12) (11 12)))

(defun creaMatriz ()
        (setf *matriz* (make-array '(11 11)))
          (loop for i from 0 to 10 do 
          (loop for j from 0 to 10 do
                   (setf (aref *matriz* i j) 
                           (if (equal (list (+ i 1) (+ j 1)) (cadr (assoc 1 pin))) 3
                                (if (contains-list (list (+ i 1) (+ j 1)) pinn) 2 
                                    (if (contains-list (list (+ i 1) (+ j 1)) pinb) 1 0)))))))

(defun convierteMatriz(array)
  (loop for i below (array-dimension array 0)
        collect (loop for j below (array-dimension array 1)
                      collect (aref array i j))))

(defun convierteLista (list)
  (make-array (list (length list)
                    (length (first list)))
              :initial-contents list))


(defun leeTablero ()
  (setq mat nil)
  (with-open-file (in "C:/Users/PC/Desktop/pruebas.txt" :direction :input )
    (loop for i from 1 to 11 do
          (setq ren nil)
          (loop for j from 1 to 11 do
               (push (read in) ren))
          (push ren mat))))


(defun creaTablero ()
      (setq pin nil) 
      (setq pinn nil)
      (setq pinb nil)
      (setq vacias nil)
      (setq idB 2)
      (setq idN 14)
      (leeTablero)
      (setq tablero (conviertelista mat))
      (loop for i from 0 to 10 do
            (loop for j from 0 to 10 do
               (cond
                        ((contains-list (list (+ i 1) (+ j 1)) reservadas) (cond ((eq 3 (aref tablero i j)) (push (list (+ i 1) (+ j 1)) pinb) (push (list 1 (list (+ i 1) (+ j 1)) 3) pin))  (t nil)) )
                        ((and (eq 0 (aref tablero i j)) (not (contains-list (list (+ i 1) (+ j 1)) reservadas))) (push (list (+ i 1) (+ j 1)) vacias))
                        ((eq 2 (aref tablero i j))  (push (list (+ i 1) (+ j 1)) pinn) (push (list idN (list (+ i 1) (+ j 1)) 2) pin) (incf idN))
                        ((eq 1 (aref tablero i j))  (push (list (+ i 1) (+ j 1)) pinb) (push (list idB (list (+ i 1) (+ j 1)) 2) pin) (incf idB))
                        (t (push (list (+ i 1) (+ j 1)) pinb) (push (list 1 (list (+ i 1) (+ j 1)) 3) pin))))) (ordenaPin))

(defun ordenaPin ()
     (setq pinAux nil)
     (dolist (x (sort (mapcar #'car pin) #'<)) (if (null x) nil (push (assoc x pin) pinAux)) )
     (setq pin (reverse pinAux)))

(defun mueveIA (dificultad)
            (creaTablero)
            (creanodo)
            (cond ((checaComer)) (t
            (alphabeta nodo dificultad alph bet 2 nil)
            (if (not (esfinal nodo dificultad)) (actualizaposicion (caar mejorRuta) (cadar mejorRuta)) nil)))
            (creaMatriz)
            (reverse (mapcar #'reverse (convierteMatriz *matriz*))))

(defun rotate (list-of-lists)
  (apply #'mapcar #'list list-of-lists))

(defun escribeResp ()         
    (with-open-file (str "C:/Users/PC/Desktop/respuesta.txt"
               :direction :output
               :if-exists :supersede
               :if-does-not-exist :create)
   (format str "~d~%~%" (mueveIA 2))))

(defun checaComer ()
    (cond 
      ( (member '(2 7) pinb :test #'equal) 
      (setq pinb (remove nil (mapcar #'(lambda (x) (when (not (equal '(2 7)x)) x)) pinb))) 
      (setq pinn (remove nil (mapcar #'(lambda (x) (when (not (equal '(1 8)x)) x)) pinn)))
      (push '(2 8) pinn)
      t
      )
      ( (member '(2 5) pinb :test #'equal) 
      (setq pinb (remove nil (mapcar #'(lambda (x) (when (not (equal '(2 5)x)) x)) pinb))) 
      (setq pinn (remove nil (mapcar #'(lambda (x) (when (not (equal '(1 4)x)) x)) pinn)))
      (push '(2 4) pinn)
      t
      )
      ( (member '(10 7) pinb :test #'equal) 
      (setq pinb (remove nil (mapcar #'(lambda (x) (when (not (equal '(10 7)x)) x)) pinb))) 
      (setq pinn (remove nil (mapcar #'(lambda (x) (when (not (equal '(11 8)x)) x)) pinn)))
      (push '(10 8) pinn)
      t
      )
      ( (member '(10 5) pinb :test #'equal) 
      (setq pinb (remove nil (mapcar #'(lambda (x) (when (not (equal '(10 5)x)) x)) pinb))) 
      (setq pinn (remove nil (mapcar #'(lambda (x) (when (not (equal '(11 4)x)) x)) pinn)))
      (push '(10 4) pinn)
      t
      )
      (t nil)    
    )
)


