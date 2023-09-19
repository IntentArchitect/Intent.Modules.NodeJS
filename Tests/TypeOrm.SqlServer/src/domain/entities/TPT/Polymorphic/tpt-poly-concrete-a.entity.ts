import { TptPoly_BaseClassNonAbstract } from './tpt-poly-base-class-non-abstract.entity';
import { Entity, Column } from 'typeorm';

@Entity('tpt_poly_concrete_a')
export class TptPoly_ConcreteA extends TptPoly_BaseClassNonAbstract {
  @Column()
  concreteField: string;

}