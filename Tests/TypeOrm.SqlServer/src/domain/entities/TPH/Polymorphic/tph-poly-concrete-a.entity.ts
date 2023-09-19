import { TphPoly_BaseClassNonAbstract } from './tph-poly-base-class-non-abstract.entity';
import { Entity, Column } from 'typeorm';

@Entity('tph_poly_concrete_a')
export class TphPoly_ConcreteA extends TphPoly_BaseClassNonAbstract {
  @Column()
  concreteField: string;

}