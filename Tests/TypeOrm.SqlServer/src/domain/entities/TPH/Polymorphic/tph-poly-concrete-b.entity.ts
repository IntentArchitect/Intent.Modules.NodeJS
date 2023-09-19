import { TphPoly_BaseClassNonAbstract } from './tph-poly-base-class-non-abstract.entity';
import { Entity, Column } from 'typeorm';

@Entity('tph_poly_concrete_b')
export class TphPoly_ConcreteB extends TphPoly_BaseClassNonAbstract {
  @Column()
  concreteField: string;

}